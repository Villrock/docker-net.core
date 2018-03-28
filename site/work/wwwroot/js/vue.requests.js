(function (Vue, VeeValidate) {

    Vue.config.devtools = true;
    if (document.querySelector("#requests")) {

        Vue.use(VeeValidate);

        var app = new Vue({
            el: "#requests",
            data: {
                textariaClass: "materialize-textarea",
                datepickerClass: "datepicker",
                autocompleteClass: "autocomplete",
                colClass: "col",
                pagingInfo: {
                    contentUrl: "",
                    currentPage: 0,
                    endIndex: 0,
                    endPage: 0,
                    itemsPerPage: 0,
                    pages: [],
                    startIndex: 0,
                    startPage: 0,
                    totalItems: 0,
                    totalPages: 0
                },
                requests: [],
                statuses: [],
                isManager: false,
                isNew: false,
                requestModel: {},
                requestDetails: [],
                scope: "Add new",
                emptyDetail: {
                    manufacturer: "",
                    model: ""
                },
                currentRequestStatus: 0,
                searchRequest: ""
            },
            created: function () {
                this.loadRequests();
            },
            methods: {
                loadRequests: function (data, isShowLoader) {
                    var self = this;
                    var subUrl = window.location.search;
                    var url = "/Request/List" + subUrl;
                    if (isShowLoader) {
                        showLoader();
                    }
                    $.ajax({
                        url: url,
                        method: "GET",
                        data: data,
                        success: function (data) {
                            self.requests = data.requests;
                            self.isManager = data.isManager;
                            self.pagingInfo = data.pagingInfo;
                            if (isShowLoader) {
                                hideLoader();
                            }
                        },
                        error: function (error) {
                            console.log(error);
                            if (isShowLoader) {
                                hideLoader();
                            }
                        }
                    });
                },
                edit: function (id) {
                    this.$validator.reset();
                    this.isNew = false;
                    this.scope = "Edit";
                    if (!id) {
                        return;
                    }
                    var self = this;
                    $.ajax({
                        url: "/Request/Get",
                        data: { id: id },
                        method: "GET",
                        success: function (data) {
                            self.requestModel = data.request;
                            self.statuses = data.statuses;
                            self.currentRequestStatus = data.currentRequestStatus;

                            self.requestModel.dateStudy = dateCorrectStr(data.request.dateStudy);

                            if (data.details && data.details.length > 0) {
                                for (var i = 0; i < data.details.length; i++) {
                                    data.details[i].shippingDate = dateCorrectStr(data.details[i].shippingDate);
                                    data.details[i].trainingDate = dateCorrectStr(data.details[i].trainingDate);
                                    data.details[i].installationDate = dateCorrectStr(data.details[i].installationDate);
                                    data.details[i].estimatedLeadTime = dateCorrectStr(data.details[i].estimatedLeadTime);

                                    data.details[i].sivRequiredDate = dateCorrectStr(data.details[i].sivRequiredDate);
                                    data.details[i].confirmedDeliveryDate = dateCorrectStr(data.details[i].confirmedDeliveryDate);
                                    data.details[i].actualDespatchDate = dateCorrectStr(data.details[i].actualDespatchDate);
                                    data.details[i].actualInstallationDate = dateCorrectStr(data.details[i].actualInstallationDate);

                                    data.details[i].issuedDate = dateCorrectStr(data.details[i].issuedDate);
                                    data.details[i].paymentDueDate = dateCorrectStr(data.details[i].paymentDueDate);
                                    data.details[i].paymentRunDate = dateCorrectStr(data.details[i].paymentRunDate);
                                    data.details[i].paymentReceivedDate = dateCorrectStr(data.details[i].paymentReceivedDate);

                                    data.details[i].serviceDueDate = dateCorrectStr(data.details[i].serviceDueDate);
                                    data.details[i].deInstallDueDate = dateCorrectStr(data.details[i].deInstallDueDate);
                                    data.details[i].estimateDeliveryDate = dateCorrectStr(data.details[i].estimateDeliveryDate);
                                }
                                self.requestDetails = data.details;
                            }
                            else {
                                self.requestDetails = [self.getEmptyDetail()];
                            }
                            setTimeout(function () {
                                initDatePicker();
                                //initRestrictionOnInputText();
                                initEventsByClosePopup();
                            }, 300);
                            self.resetInvoiceFiles();
                            updateWindowHref(id);
                        },
                        error: function (error) { console.log(error); }
                    });
                },
                dateCorrectStr: function (date) {
                    return dateCorrectStr(date);
                },
                update: function () {
                    if (this.isNew) {
                        this.updateRequest("/Request/New");
                        return;
                    }
                    var self = this;
                    var hasInvoiceFiles = function () {
                        var result = true;
                        for (var i = 0; i < self.requestDetails.length; i++) {
                            if (!self.requestDetails[i].invoiceFile) {
                                $('#InvoiceFile-text' + i).addClass('input-validation-error');
                                $('#InvoiceFile-text-error' + i).removeClass('hide');
                                result = false;
                            }
                        }
                        return result;
                    }

                    if (this.requestModel.status > 30 && !hasInvoiceFiles()) {
                        return;
                    }
                    this.updateRequest("/Request/Set");
                },
                updateRequest: function (url) {
                    var self = this;
                    this.requestModel.details = this.requestDetails;
                    this.$validator.validateAll().then(result => {
                        if (!result) {
                            return;
                        }
                        var saveRequestFunc = function () {
                            $.ajax({
                                url: url,
                                data: self.requestModel,
                                method: "POST",
                                success: function () {
                                    self.reset();
                                    self.reloadListWithPagingAndSearch();
                                },
                                error: function (error) { console.log(error); }
                            });
                        };
                        //save files
                        saveFiles("input[type=file]", saveRequestFunc);
                    });
                },
                changeBrocureHandler: function (e, model) {
                    if (e.which <= 90 && e.which >= 48 || e.which === 8) {
                        var target = $(e.target);
                        if (target.val()) {
                            var url = "/Document/GetList/";
                            var data = { search: target.val() }
                            this.autocompleteLoad(target, url, data, function (val) {
                                model.linkToBrochure = val;
                            });
                        }
                    }
                },
                reloadListWithPagingAndSearch: function (data) {
                    var showLoader = false;
                    if (data) {
                        data.search = this.searchRequest ? this.searchRequest : "";
                        showLoader = !!this.searchRequest;
                    } else {
                        data = { search: this.searchRequest ? this.searchRequest : "" };
                    }
                    this.loadRequests(data, showLoader);
                },
                changeSearchHandler: function (e) {
                    this.reloadListWithPagingAndSearch();
                },
                changeCurrencyHandler: function (e, model) {
                    if (e.which <= 90 && e.which >= 48 || e.which === 8) {
                        var target = $(e.target);
                        if (target.val()) {
                            var url = "/Request/GetCurrencies/";
                            var data = { search: target.val() }
                            this.autocompleteLoad(target, url, data, function (val) {
                                model.currency = val;
                            });
                        }
                    }
                },
                changeManufacturerHandler: function (e, model) {
                    if (e.which <= 90 && e.which >= 48 || e.which === 8) {
                        var target = $(e.target);
                        var value = target.val();
                        if (value && value.length > 1) {
                            var url = "/Request/GetManufacturers/";
                            var data = { search: value }
                            this.autocompleteLoad(target, url, data, function (val) {
                                model.manufacturer = val;
                            });
                        }
                    }
                },
                changeModelHandler: function (e, model) {
                    if (e.which <= 90 && e.which >= 48 || e.which === 8 || e.which <= 111 && e.which >= 106) {
                        var target = $(e.target);
                        if (target.val()) {
                            var url = "/Request/GetManufacturerModels/";
                            var data = {
                                search: target.val(),
                                searchManufacture: model.manufacturer
                            }
                            this.autocompleteLoad(target, url, data, function (val) {
                                model.model = val;
                            });
                        }
                    }
                },
                autocompleteLoad: function (target, url, dataSend, onAutocomplete) {
                    $.ajax({
                        url: url,
                        method: "GET",
                        data: dataSend,
                        success: function (data) {
                            var dataSource = {};
                            for (var i = 0; i < data.length; i++) {
                                dataSource[data[i].name] = null;
                            }

                            target.autocomplete({
                                data: dataSource,
                                limit: 10,
                                minLength: 1,
                                onAutocomplete: onAutocomplete
                            });
                        },
                        error: function (error) { console.log(error); }
                    });
                },
                uploadFile: function (fileInputId, modelUpdateFunc) {
                    uploadFileAndChangeModel(fileInputId, modelUpdateFunc);
                },
                setStatus: function (status) {
                    this.requestModel.status = status.id;
                    this.requestModel.statusText = status.name;
                },
                setCorrectDate: function (date) {
                    return dateCorrectStr(date);
                },
                setNew: function () {
                    this.$validator.reset();
                    this.requestModel = {};
                    this.scope = "Add new";
                    this.requestDetails = [this.getEmptyDetail()];
                    this.isNew = true;
                    this.currentRequestStatus = 0;
                    this.resetInvoiceFiles();
                },
                reset: function () {
                    this.requestModel = {};
                    this.$validator.reset();
                    $(".modal").modal("close");
                },
                addDetail: function () {
                    this.requestDetails.push(this.getEmptyDetail());
                    setTimeout(function () {
                        initDatePicker();
                        //initRestrictionOnInputText();
                    }, 300);
                },
                getEmptyDetail: function () {
                    return $.extend({}, this.emptyDetail);
                },
                isShowStatuses: function () {
                    return this.isManager || (this.requestModel.status && this.requestModel.status !== 10);
                },
                setCorrectErrorName: function (message, oldName, newName) {
                    if (message) {
                        return message.replace(oldName, newName);
                    }
                    return message;
                },
                resetInvoiceFiles: function () {
                    for (var i = 0; i < this.requestDetails.length; i++) {
                        if (!this.requestDetails[i].invoiceFile) {
                            $('#InvoiceFile-text' + i).removeClass('input-validation-error');
                            $('#InvoiceFile-text-error' + i).addClass('hide');
                        }
                    }
                }
            }
        });

        function initDatePicker() {

            function setDateFromDatePicker(nodeId, value, index) {
                if (nodeId.indexOf('datestudy') > -1)
                    app.requestModel.dateStudy = value;
                /*quoted*/
                if (nodeId.indexOf('shippingdate') > -1)
                    app.requestDetails[index].shippingDate = value;
                if (nodeId.indexOf('trainingdate') > -1)
                    app.requestDetails[index].trainingDate = value;
                if (nodeId.indexOf('installationdate') > -1)
                    app.requestDetails[index].installationDate = value;
                if (nodeId.indexOf('estimatedleadtime') > -1)
                    app.requestDetails[index].estimatedLeadTime = value;
                /*Purchase Order*/
                if (nodeId.indexOf('serviceduedate') > -1)
                    app.requestDetails[index].serviceDueDate = value;
                if (nodeId.indexOf('deinstallduedate') > -1)
                    app.requestDetails[index].deInstallDueDate = value;
                if (nodeId.indexOf('sivrequireddate') > -1)
                    app.requestDetails[index].sivRequiredDate = value;
                if (nodeId.indexOf('confirmeddeliverydate') > -1)
                    app.requestDetails[index].confirmedDeliveryDate = value;
                if (nodeId.indexOf('actualdespatchdate') > -1)
                    app.requestDetails[index].actualDespatchDate = value;
                if (nodeId.indexOf('actualinstallationdate') > -1)
                    app.requestDetails[index].actualInstallationDate = value;
                if (nodeId.indexOf('estimatedeliverydate') > -1)
                    app.requestDetails[index].estimateDeliveryDate = value;
                /*Invoiced*/
                if (nodeId.indexOf('issueddate') > -1)
                    app.requestDetails[index].issuedDate = value;
                if (nodeId.indexOf('paymentduedate') > -1)
                    app.requestDetails[index].paymentDueDate = value;
                if (nodeId.indexOf('paymentrundate') > -1)
                    app.requestDetails[index].paymentRunDate = value;
                /*Invoiced*/
                if (nodeId.indexOf('paymentreceiveddate') > -1)
                    app.requestDetails[index].paymentReceivedDate = value;
                app.$validator.reset();
            };

            $('.datepicker').pickadate({
                container: 'body',
                selectMonths: true, // Creates a dropdown to control month
                selectYears: 15, // Creates a dropdown of 15 years to control year,
                today: 'Today',
                clear: 'Clear',
                close: 'Ok',
                format: 'yyyy-mm-dd',
                closeOnSelect: true,
                onSet: function () {
                    var nodeId = this.$node[0].id.toLowerCase();
                    var index = nodeId.slice(-1);

                    setDateFromDatePicker(nodeId, this.get(), index);
                },
                onOpen: function () {
                    if (this.get()) {
                        return;
                    }
                    this.clear();
                }
            });
        }

        $(document).ready(function () {
            function getHeshUrlParameter(sParam) {
                var sPageUrl = window.location.href;
                var sUrlVariables = sPageUrl.split('#');
                for (var i = 0; i < sUrlVariables.length; i++) {
                    var sParameterName = sUrlVariables[i].split('=');
                    if (sParameterName[0] === sParam) {
                        return sParameterName[1];
                    }
                }
            };

            var request = getHeshUrlParameter('request');
            if (request) {
                var modalEl = $('#editRequestModal');
                if (modalEl) {
                    setTimeout(function () {
                        app.edit(request);
                        app.isNew = false;
                        modalEl.modal('open');
                        initEventsByClosePopup();
                    }, 500);

                }
            }
            initDatePicker();

            //search
            $('#searchRequest').keyup($.debounce(350, app.changeSearchHandler));
        });
    }
})(Vue, VeeValidate);