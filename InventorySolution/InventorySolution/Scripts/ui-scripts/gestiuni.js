$.extend({
    Gestiuni: {
        parameters: {},
        Parameters: function(a) {
            var params = $.extend($.Gestiuni.parameters, a);
        },
        Init: function(a){
            //(function ($) {
                "use strict";

                var Address = function (options) {
                    this.init('address', options, Address.defaults);
                };

                //inherit from Abstract input
                $.fn.editableutils.inherit(Address, $.fn.editabletypes.abstractinput);

                $.extend(Address.prototype, {
                    /**
                    Renders input from tpl
                    
                    **/
                    render: function () {
                        this.$input = this.$tpl.find('input');
                    },

                    /**
                    Default method to show value in element. Can be overwritten by display option.
                    **/
                    value2html: function (value, element) {
                        if (!value) {
                            $(element).empty();
                            return;
                        }
                        var html = $('<div>').text(value.nume).html() + ' ' + $('<div>').text(value.prenume).html() + ' CATEDRA: ' + $('<div>').text(value.catedra).html();
                        $(element).html(html);
                    },

                    /**
                    Gets value from element's html
                    **/
                    html2value: function (html) {
                        /*
                          you may write parsing method to get value by element's html
                          e.g. "Moscow, st. Lenina, bld. 15" => {city: "Moscow", street: "Lenina", building: "15"}
                          but for complex structures it's not recommended.
                          Better set value directly via javascript, e.g. 
                          editable({
                              value: {
                                  city: "Moscow", 
                                  street: "Lenina", 
                                  building: "15"
                              }
                          });
                        */
                        return null;
                    },

                    /**
                     Converts value to string. 
                     It is used in internal comparing (not for sending to server).
                    **/
                    value2str: function (value) {
                        var str = '';
                        if (value) {
                            for (var k in value) {
                                str = str + k + ':' + value[k] + ';';
                            }
                        }
                        return str;
                    },

                    /*
                     Converts string to value. Used for reading value from 'data-value' attribute.
                    */
                    str2value: function (str) {
                        /*
                        this is mainly for parsing value defined in data-value attribute. 
                        If you will always set value by javascript, no need to overwrite it
                        */
                        return str;
                    },

                    /**
                     Sets value of input.
                    **/
                    value2input: function (value) {
                        if (!value) {
                            return;
                        }
                        this.$input.filter('[name="nume"]').val(value.nume);
                        this.$input.filter('[name="prenume"]').val(value.prenume);
                        this.$input.filter('[name="catedra"]').val(value.catedra);
                    },

                    /**
                     Returns value of input.
                    **/
                    input2value: function () {
                        return {
                            nume: this.$input.filter('[name="nume"]').val(),
                            prenume: this.$input.filter('[name="prenume"]').val(),
                            catedra: this.$input.filter('[name="catedra"]').val()
                        };
                    },

                    /**
                    Activates input: sets focus on the first field.
                   **/
                    activate: function () {
                        this.$input.filter('[name="nume"]').focus();
                    },

                    /**
                     Attaches handler to submit form in case of 'showbuttons=false' mode
                    **/
                    autosubmit: function () {
                        this.$input.keydown(function (e) {
                            if (e.which === 13) {
                                $(this).closest('form').submit();
                            }
                        });
                    }
                });

                Address.defaults = $.extend({}, $.fn.editabletypes.abstractinput.defaults, {
                    tpl: '<div class="row">' +
                            '<div class="col-md-12 editable-input">' +
                                '<span>Nume:</span>' +
                            '</div>' +
                            '<div class="col-md-12">' +
                                '<input type="text" name="nume" class="form-control input-sm">' +
                            '</div>' +
                        '</div>' +
                        '<div class="row">' +
                            '<div class="col-md-12 editable-input">' +
                                '<span>Prenume:</span>' +
                            '</div>' +
                            '<div class="col-md-12">' +
                                '<input type="text" name="prenume" class="form-control input-sm">' +
                            '</div>' +
                        '</div>' +
                        '<div class="row">' +
                            '<div class="col-md-12 editable-input">' +
                                '<span>Catedra:</span>' +
                            '</div>' +
                            '<div class="col-md-12">' +
                                '<input type="text" name="catedra" class="form-control input-sm">' +
                            '</div>' +
                        '</div>',
                    inputclass: ''
                });
                $.fn.editabletypes.address = Address;
                //}(window.jQuery));
            },
            
        MakeEditables: function (a) {
            var params = $.extend($.Gestiuni.parameters, a);
            $.fn.editable.defaults.mode = 'inline';
            $.fn.editable.defaults.showbuttons = 'top';
            $.fn.editable.defaults.send = 'always';
            //$.fn.editable.defaults.onblur = 'ignore';

            $.fn.editableform.errorGroupClass = 'alert alert-error';

            var editopts = {
                savenochange: true,
                url: "/Gestiuni/AddOrUpdateGestiune",
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
                    $(this).closest(".gestiune").find('.gestiune-id:first').html("#" + response.Id);
                    if (response.Message != null && response.Message != '') {
                        alert(response.Message);
                    }
                    //location.reload();// refresh page
                },
                display: function (value) {
                    if (!value) {
                        $(this).empty();
                        return;
                    }
                    var html = "<b>" + $('<div>').text(value.nume).html() + ' ' + $('<div>').text(value.prenume).html() + "</b>" + ', Catedra: <b> ' + $('<div>').text(value.catedra).html() + "</b>";
                    $(this).html(html);
                }
            };

            $(".gestiune-body").editable(editopts);
            $('.edit-gestiune-button').on('click', function (e) {
                e.preventDefault();
                e.stopPropagation();
                var $editable = $(this).closest('.gestiune').find('.editable');
                $editable.editable('toggle');
            });
        },
        
        MakeEditableLastAddedItem: function (a) {
            var params = $.extend($.Gestiuni.parameters, a);
            $.fn.editable.defaults.mode = 'inline';
            $.fn.editable.defaults.showbuttons = 'top';
            $.fn.editable.defaults.send = 'always';
            $.fn.editable.defaults.onblur = 'ignore';

            $.fn.editableform.errorGroupClass = 'alert alert-error';

            var editopts = {
                savenochange: true,
                url: "/Gestiuni/AddOrUpdateGestiune",
                params: function (params) {
                     //add additional params from data-attributes of trigger element
                    return params;
                },
                validate: function (value) {
                    var msg = '';
                    if (value.nume == '') msg += 'Nume is required! \n';
                    if (value.prenume == '') msg += 'Prenume is required! \n'; 
                    if (value.catedra == '') msg += 'Catedra is required! \n';
                    return msg;
                },
                success: function (response, newValue) {
                    $(this).html(newValue); // replace value
                     //replace id
                    $(this).closest(".gestiune").find('.gestiune-id:first').html("#" + response.Id);
                    //location.reload();// refresh page
                },
                display: function (value) {
                if (value.nume == '' && value.prenume == '' && value.catedra == '' ) {
                    $(this).empty();
                    return;
                }
                var html = "<b>" + $('<div>').text(value.nume).html() + ' ' + $('<div>').text(value.prenume).html() + "</b>" + ', Catedra: <b> ' + $('<div>').text(value.catedra).html() + "</b>";
                $(this).html(html);
            }
            };

            $(".gestiune-body:last").editable(editopts);
            $('.edit-gestiune-button').on('click', function (e) {
                e.preventDefault();
                e.stopPropagation();
                var $editable = $(this).closest('.gestiune').find('.editable');
                $editable.editable('toggle');
            });
        }
    }
});