jQuery(function () {
    initSelect();
    //initRestrictionOnInputText();
    jQuery('.modal').modal();
    jQuery('.dropdown-button').dropdown();

    jQuery(".notifications-toggle").sideNav({
        menuWidth: 400,
        edge: 'right'
    });

    jQuery('.notifications-close').on('click', function () {
        jQuery(this).sideNav('hide');
    });

    jQuery(".notifications-toggle").on('click', function () {
        var infoClass = 'info';
        var opener = jQuery(this);
        var parent = opener.closest('li');
        var panelItems = jQuery('#' + opener.attr('data-activates')).find('.notifications-list li');
        var timer = setTimeout(removeItemClass, 3000);

        function removeItemClass() {
            if (parent.hasClass(infoClass)) {
                parent.removeClass(infoClass);
            }

            panelItems.each(function () {
                jQuery(this).removeClass('active');
            });

            $.ajax({
                url: "/Notification/SetAsReaded",
                method: "POST",
                error: function (error) {
                    console.log(error);
                }
            });
        }
    });

    jQuery('input[name="requestGroup"]').on('click', function () {
        var status = $(this).attr('data-status');
        if (status) {
            window.location.href = window.location.protocol + "//" + window.location.host + "/?status=" + status;
        }
    });
});

function dateCorrectStr(dateStr) {
    if (dateStr) {
        var date = new Date(dateStr);
        var yyyy = date.getFullYear().toString();
        var mm = (date.getMonth() + 1).toString(); // getMonth() is zero-based         
        var dd = date.getDate().toString();
        return yyyy + "-" + mm + "-" + dd;
    }
    return '';
}

function updateWindowHref(id) {
    var location = window.location.href.split('#')[0];
    window.location = location + "#request=" + id;
}

function initSelect() {
    $('select').material_select();
}

//function initRestrictionOnInputText() {
//    $('input[type="text"]').on('keypress', function (e) {
//        var txt = String.fromCharCode(e.which);
//        if (txt.match(/[A-Za-z0-9&. ]/) || e.which === 8 || e.which === 16 || (e.which > 41 && e.which < 47)) {
//            return true;
//        }
//        return false;
//    });
//}

function initEventsByClosePopup() {
    $('.modal-overlay').on('click', function () {
        var location = window.location.href.split('#')[0];
        window.location = location + "#";
        $('input[type=file]').val("");
    });

    $('.update-hash-url').on('click', function () {
        $('input[type=file]').val("");
    });
}

function hasFiles(fileUploads) {
    var has = false;
    fileUploads.each(function (index, fileUpload) {
        has = has || fileUpload.files.length > 0;
    });
    return has;
}

function saveFiles(selector, afterSuccessUploadFunc) {
    var fileUploads = $(selector);
    if (fileUploads.length > 0 && hasFiles(fileUploads)) {
        var data = new FormData();
        for (var i = 0; i < fileUploads.length; i++) {
            var files = fileUploads[i].files;
            if (files.length) {
                data.append("Files", files[0]);
                data.append("FilePaths", $(fileUploads[i]).attr("data-path"));
            }
        }

        $.ajax({
            type: "POST",
            url: "/Request/SaveFiles",
            contentType: false,
            processData: false,
            data: data,
            success: function () { afterSuccessUploadFunc(); },
            error: function (error) { console.log(error); }
        });
    } else {
        afterSuccessUploadFunc();
    }
}
function changeFile(target, changeModelFunc) {
    var inputFile = $(target);
    var hasFile = inputFile[0].files.length > 0;
    if (hasFile) {
        var label = inputFile[0].files[0].name;
        var size = inputFile[0].files[0].size;

        var errorSpan = inputFile.siblings(".file-path-wrapper").find('.red-text');
        if (size > 10000000) {
            errorSpan.text("Max size 10Mb");
            errorSpan.removeClass('hide');
            inputFile.val("");
        } else {
            errorSpan.addClass('hide');
            changeModelFunc(label);
        }
    }
}

function uploadFileAndChangeModel(fileInputId, changeModelFunc) {
    var fileInput = $("#" + fileInputId);
    if (fileInput) {
        fileInput.on('change', function () {
            changeFile(this, changeModelFunc);
            $(this).off('change');
        });
        fileInput.click();
    }
}

function hideLoader() {
    var loader = $('.main-loader');
    if (loader) {
        loader.addClass('hide');
    }
}
function showLoader() {
    var loader = $('.main-loader');
    if (loader) {
        loader.removeClass('hide');
    }
}
