(function (Vue, VeeValidate) {

    Vue.config.devtools = true;
    if (document.querySelector("#documentVault")) {

        Vue.use(VeeValidate);

        var app = new Vue({
            el: "#documentVault",
            data: {
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
                documents: [],
                searchRequest: ""
            },
            created: function () {
                this.loadDocuments();
            },
            methods: {
                loadDocuments: function (data, isShowLoader) {
                    var self = this;
                    var url = "/Document/List";
                    if (isShowLoader) {
                        showLoader();
                    }
                    $.ajax({
                        url: url,
                        method: "GET",
                        data: data,
                        success: function (data) {
                            self.documents = data.documents;
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
                reloadListWithPagingAndSearch: function (data) {
                    var showLoader = false;
                    if (data) {
                        data.search = this.searchRequest ? this.searchRequest : "";
                        showLoader = !!this.searchRequest;
                    } else {
                        data = { search: this.searchRequest ? this.searchRequest : "" };
                    }
                    this.loadDocuments(data, showLoader);
                },
                changeSearchHandler: function (e) {
                    this.reloadListWithPagingAndSearch();
                }
            }
        });

        $(document).ready(function () {
            //search
            $('#searchRequest').keyup($.debounce(350, app.changeSearchHandler));
        });
    }
})(Vue, VeeValidate);