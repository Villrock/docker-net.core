(function (Vue, VeeValidate) {

    if (document.querySelector('#clients')) {
        Vue.use(VeeValidate);

        var appClient = new Vue({
            el: "#clients",
            data: {
                manageList: [],
                user: {
                    firstNmae: '',
                    lastName: '',
                    email: '',
                    password: '',
                    confirmPassword:''
                },
                emptyUser: {
                    firstNmae: '',
                    lastName: '',
                    email: '',
                    password: '',
                    confirmPassword: ''
                }
            },
            created: function () {
                this.loadData();
            },
            methods: {
                loadData: function () {
                    var self = this;
                    var subUrl = window.location.search;
                    var url = '/Manage/ClientList' + subUrl;
                    $.ajax({
                        url: url,
                        method: 'GET',
                        success: function (data) {
                            self.manageList = data;
                        },
                        error: function (error) {
                            console.log(error);
                        }
                    });
                },
                edit: function (id) {
                    editClientModal.edit(id);
                },
                resetData: function () {
                    if (editClientModal && editClientModal.resetData)
                        editClientModal.resetData();
                    if (addClientModal && addClientModal.resetData)
                        addClientModal.resetData();
                }
            }
        });

        var addClientModal = new Vue({
            el: "#clientsAddNewModel",
            data: {
                scope: 'Client',
                user: {
                    firstNmae: '',
                    lastName: '',
                    email: '',
                    password: '',
                    confirmPassword: ''
                },
                emptyUser: {
                    firstNmae: '',
                    lastName: '',
                    email: '',
                    password: '',
                    confirmPassword: ''
                }
            },
            methods: {
                save: function () {
                    var self = this;
                    this.$validator.validateAll().then(result => {
                        if (result) {
                            $.ajax({
                                url: '/Manage/SaveClient',
                                data: this.user,
                                method: 'POST',
                                success: function () {
                                    self.reset();
                                    appClient.loadData();
                                },
                                error: function (error) {
                                    console.log(error);
                                }
                            });
                        }
                    });
                },
                reset: function () {
                    this.resetData();
                    $('.modal').modal('close');
                },
                resetData: function () {
                    this.user = this.emptyUser;
                    this.$validator.reset();
                },
                setCorrectErrorName: function (message, oldName, newName) {
                    if (message) {
                        return message.replace(oldName, newName);
                    }
                    return message;
                }
            }
        });

        var editClientModal = new Vue({
            el: "#clientsEditModal",
            data: {
                scope: 'Client',
                user: {
                    firstNmae: '',
                    lastName: '',
                    email: ''
                },
                emptyUser: {
                    firstNmae: '',
                    lastName: '',
                    email: ''
                }
            },
            methods: {
                save: function () {
                    var self = this;
                    this.$validator.validateAll().then(result => {
                        if (result) {
                            $.ajax({
                                url: '/Manage/SaveClient',
                                data: this.user,
                                method: 'POST',
                                success: function () {
                                    self.reset();
                                    appClient.loadData();
                                },
                                error: function (error) {
                                    console.log(error);
                                }
                            });
                        }
                    });
                },
                edit: function (id) {
                    this.resetData();
                    if (id) {
                        var self = this;
                        $.ajax({
                            url: '/Manage/GetClient',
                            data: { id: id },
                            method: 'GET',
                            success: function (data) {
                                self.user = data;
                            },
                            error: function (error) {
                                console.log(error);
                            }
                        });
                    }
                },
                reset: function () {
                    this.resetData();
                    $('.modal').modal('close');
                },
                resetData: function () {
                    this.user = this.emptyUser;
                    this.$validator.reset();
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
    else if (document.querySelector('#managers')) {
        Vue.use(VeeValidate);

        var appManager = new Vue({
            el: "#managers",
            data: {
                manageList: [],
                user: {
                    firstNmae: '',
                    lastName: '',
                    email: '',
                    password: '',
                    confirmPassword: ''
                },
                emptyUser: {
                    firstNmae: '',
                    lastName: '',
                    email: '',
                    password: '',
                    confirmPassword: ''
                }
            },
            created: function () {
                this.loadData();
            },
            methods: {
                loadData: function () {
                    var self = this;
                    var subUrl = window.location.search;
                    var url = '/Manage/ManagerList' + subUrl;
                    $.ajax({
                        url: url,
                        method: 'GET',
                        success: function (data) {
                            self.manageList = data;
                        },
                        error: function (error) {
                            console.log(error);
                        }
                    });
                },
                edit: function (id) {
                    editManagerModal.edit(id);
                },
                resetData: function () {
                    editManagerModal.resetData();
                    addManagerModal.resetData();
                }
            }
        });

        var addManagerModal = new Vue({
            el: "#managersAddNewModel",
            data: {
                scope: 'Manager',
                user: {
                    firstNmae: '',
                    lastName: '',
                    email: '',
                    password: '',
                    confirmPassword: ''
                },
                emptyUser: {
                    firstNmae: '',
                    lastName: '',
                    email: '',
                    password: '',
                    confirmPassword: ''
                }
            },
            methods: {
                save: function () {
                    var self = this;
                    this.$validator.validateAll().then(result => {
                        if (result) {
                            $.ajax({
                                url: '/Manage/SaveManager',
                                data: this.user,
                                method: 'POST',
                                success: function () {
                                    self.reset();
                                    appManager.loadData();
                                },
                                error: function (error) {
                                    console.log(error);
                                }
                            });
                        }
                    });
                },
                reset: function () {
                    this.resetData();
                    $('.modal').modal('close');
                },
                resetData: function () {
                    this.user = this.emptyUser;
                    this.$validator.reset();
                },
                setCorrectErrorName: function (message, oldName, newName) {
                    if (message) {
                        return message.replace(oldName, newName);
                    }
                    return message;
                }
            }
        });

        var editManagerModal = new Vue({
            el: "#managersEditModal",
            data: {
                scope: 'Manager',
                user: {
                    firstNmae: '',
                    lastName: '',
                    email: ''
                },
                emptyUser:{
                firstNmae: '',
                lastName: '',
                email: ''
            }
            },
            methods: {
                save: function () {
                    var self = this;
                    this.$validator.validateAll().then(result => {
                        if (result) {
                            $.ajax({
                                url: '/Manage/SaveManager',
                                data: this.user,
                                method: 'POST',
                                success: function () {
                                    self.reset();
                                    appManager.loadData();
                                },
                                error: function (error) {
                                    console.log(error);
                                }
                            });
                        }
                    });
                },
                edit: function (id) {
                    this.resetData();
                    if (id) {
                        var self = this;
                        $.ajax({
                            url: '/Manage/GetManager',
                            data: { id: id },
                            method: 'GET',
                            success: function (data) {
                                self.user = data;
                            },
                            error: function (error) {
                                console.log(error);
                            }
                        });
                    }
                },
                reset: function () {
                    this.resetData();
                    $('.modal').modal('close');
                },
                resetData: function () {
                    this.user = this.emptyUser;
                    this.$validator.reset();
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