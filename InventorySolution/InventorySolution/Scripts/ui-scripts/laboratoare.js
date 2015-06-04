$.extend({
    Laboratoare: {
        parameters: {},
        Parameters: function(a) {
            var params = $.extend($.Laboratoare.parameters, a);
        },
        MakeEditables: function (a) {
            var params = $.extend($.Laboratoare.parameters, a);
            $.fn.editable.defaults.mode = 'inline';
            $.fn.editable.defaults.showbuttons = 'top';
            $.fn.editable.defaults.send = 'always';
            //$.fn.editable.defaults.onblur = 'ignore';

            $.fn.editableform.errorGroupClass = 'alert alert-error';

            var editopts = {
                savenochange: true,
                url: "/Laboratoare/AddOrUpdateLaborator",
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
                    $(this).closest(".laborator").find('.laborator-id:first').html("#" + response.Id);
                    //location.reload();// refresh page
                }
            };

            $(".laborator-body").editable(editopts);
            $('.edit-laborator-button').on('click', function (e) {
                e.preventDefault();
                e.stopPropagation();
                var $editable = $(this).closest('.laborator').find('.editable');
                $editable.editable('toggle');
            });
        },

        MakeEditableLastAddedItem: function (a) {
            var params = $.extend($.Laboratoare.parameters, a);
            $.fn.editable.defaults.mode = 'inline';
            $.fn.editable.defaults.showbuttons = 'top';
            $.fn.editable.defaults.send = 'always';
            //$.fn.editable.defaults.onblur = 'ignore';

            $.fn.editableform.errorGroupClass = 'alert alert-error';

            var editopts = {
                savenochange: true,
                url: "/Laboratoare/AddOrUpdateLaborator",
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
                    $(this).closest(".laborator").find('.laborator-id:first').html("#" + response.Id);
                    //location.reload();// refresh page
                }
            };

            $(".laborator-body:last").editable(editopts);
            $('.edit-laborator-button').on('click', function (e) {
                e.preventDefault();
                e.stopPropagation();
                var $editable = $(this).closest('.laborator').find('.editable');
                $editable.editable('toggle');
            });
        }
    }
});