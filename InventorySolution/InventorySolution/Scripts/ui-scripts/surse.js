//$(document).ready(function () {
//    var serviceURL = '/Surse/FirstAjax';
//    $.ajax({
//        type: "POST",
//        url: serviceURL,
//        data: param = "",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: successFunc,
//        error: errorFunc
//    });

//    function successFunc(data, status) {
//        alert(data);
//    }

//    function errorFunc() {
//        alert('error');
//    }
    //});
//});

$.extend({
    Surse: {
        parameters: {},
        Parameters: function(a) {
            var params = $.extend($.Surse.parameters, a);
        },
        MakeEditables: function (a) {
            var params = $.extend($.Surse.parameters, a);
            $.fn.editable.defaults.mode = 'inline';
            $.fn.editable.defaults.showbuttons = 'top';
            $.fn.editable.defaults.send = 'always';
            //$.fn.editable.defaults.onblur = 'ignore';

            $.fn.editableform.errorGroupClass = 'alert alert-error';

            var editopts = {
                savenochange: true,
                url: "/Surse/AddOrUpdateSursa",
                params: function (params) {
                    // add additional params from data-attributes of trigger element
                    return params;
                },
                validate: function (value) {
                    if (value == '') return 'Text is required!';
                    return '';
                },
                success: function (response, newValue) {
                    $(this).html(newValue); //replace value
                    // replace id
                    $(this).closest(".sursa").find('.sursa-id:first').html("#" + response.Id);
                    location.reload();// refresh page
                }
            };

            $(".sursa-body").editable(editopts);
            $('.edit-sursa-button').on('click', function (e) {
                e.preventDefault();
                e.stopPropagation();
                var $editable = $(this).closest('.sursa').find('.editable');
                $editable.editable('toggle');
            });
        },

        MakeEditableLastAddedItem: function (a) {
            var params = $.extend($.Surse.parameters, a);
            $.fn.editable.defaults.mode = 'inline';
            $.fn.editable.defaults.showbuttons = 'top';
            $.fn.editable.defaults.send = 'always';
            //$.fn.editable.defaults.onblur = 'ignore';

            $.fn.editableform.errorGroupClass = 'alert alert-error';

            var editopts = {
                savenochange: true,
                url: "/Surse/AddOrUpdateSursa",
                params: function (params) {
                    // add additional params from data-attributes of trigger element
                    return params;
                },
                validate: function (value) {
                    if (value == '') return 'Text is required!';
                    return '';
                },
                success: function (response, newValue) {
                    $(this).html(newValue); // replace value
                    // replace id
                    $(this).closest(".sursa").find('.sursa-id:first').html("#" + response.Id);
                    location.reload();// refresh page
                }
            };

            $(".sursa-body:last").editable(editopts);
            $('.edit-sursa-button').on('click', function (e) {
                e.preventDefault();
                e.stopPropagation();
                var $editable = $(this).closest('.sursa').find('.editable');
                $editable.editable('toggle');
            });
        }
    }
});