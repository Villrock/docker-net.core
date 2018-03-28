(function (Vue, VeeValidate) {

    if (document.querySelector("#alerts")) {
        Vue.use(VeeValidate);

        var appAlerts = new Vue({
            el: "#alerts",
            data: {
                textariaClass: "materialize-textarea",
                colClass: "col",
                alerts: [],
                notifyTimes: []
            },
            created: function () {
                this.loadAlerts();
            },
            methods: {
                loadAlerts: function () {
                    var self = this;
                    var url = "/Manage/AlertList";
                    $.ajax({
                        url: url,
                        method: "GET",
                        success: function (data) {
                            self.notifyTimes = data.notifyTimes;
                            self.alerts = data.alerts;
                            setTimeout(initSelect, 100);
                        },
                        error: function (error) {
                            console.log(error);
                        }
                    });
                },
                save: function (alert, index) {
                    this.$validator.validateAll().then(result => {
                        if (result) {
                            alert.notifyDays = $("#TimeAlert" + index).val();
                            $.ajax({
                                url: "/Manage/SaveAlert",
                                data: alert,
                                method: "POST",
                                error: function (error) {
                                    console.log(error);
                                }
                            });
                        }
                    });
                },
                cancel: function () {
                    this.$validator.reset();
                    this.loadAlerts();
                },
                setCorrectErrorName: function (message, oldName, newName) {
                    if (message) {
                        return message.replace(oldName, newName);
                    }
                    return message;
                }
            }
        });
    }
})(Vue, VeeValidate);