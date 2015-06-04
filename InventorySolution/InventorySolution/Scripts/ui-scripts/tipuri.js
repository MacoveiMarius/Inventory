$.extend({
    Tipuri: {
        parameters: {},
        Parameters: function(a) {
            var params = $.extend($.Tipuri.parameters, a);
        },
        MakeEditables: function (a) {
            var params = $.extend($.Tipuri.parameters, a);
            $.fn.editable.defaults.mode = 'inline';
            $.fn.editable.defaults.showbuttons = 'top';
            $.fn.editable.defaults.send = 'always';
            //$.fn.editable.defaults.onblur = 'ignore';

            $.fn.editableform.errorGroupClass = 'alert alert-error';

            var editopts = {
                savenochange: true,
                url: "/Tipuri/AddOrUpdateTip",
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
                    $(this).closest(".tip").find('.tip-id:first').html("#" + response.Id);
                    //location.reload();// refresh page
                }
            };

            $(".tip-body").editable(editopts);
            $('.edit-tip-button').on('click', function (e) {
                e.preventDefault();
                e.stopPropagation();
                var $editable = $(this).closest('.tip').find('.editable');
                $editable.editable('toggle');
            });
        },

        MakeEditableLastAddedItem: function (a) {
            var params = $.extend($.Tipuri.parameters, a);
            $.fn.editable.defaults.mode = 'inline';
            $.fn.editable.defaults.showbuttons = 'top';
            $.fn.editable.defaults.send = 'always';
            //$.fn.editable.defaults.onblur = 'ignore';

            $.fn.editableform.errorGroupClass = 'alert alert-error';

            var editopts = {
                savenochange: true,
                url: "/Tipuri/AddOrUpdateTip",
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
                    $(this).closest(".tip").find('.tip-id:first').html("#" + response.Id);
                    //location.reload();// refresh page
                }
            };

            $(".tip-body:last").editable(editopts);
            $('.edit-tip-button').on('click', function (e) {
                e.preventDefault();
                e.stopPropagation();
                var $editable = $(this).closest('.tip').find('.editable');
                $editable.editable('toggle');
            });
        }
    }
});