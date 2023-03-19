$(function () {
    var CKEDITOR_BASEPATH = "/content/plugin/ckeditor";
    CheckBox();
    RadioBox();
    Tooltip();
    KodNumber.Load();
    EnterDisabled();
    Show_Hide_Event(); 
    TextareaLength(); 

});

 

function CheckBox() {
    var checkbox = $('.kod-checkbox label');
    checkbox.click(function () {
        if ($(this).hasClass("active")) {
            $(this).removeClass("active");
        } else {
            $(this).addClass("active");
        }
    });
}

function CheckBoxAll() {
    var checkbox = $('.kod-checkbox label');
    checkbox.click(function () {
        if ($(this).parents('.kod-checkbox').hasClass('checkall')) {
            var tableCheckBoxs = $('table.table tbody tr td .kod-checkbox label');
            if ($(this).hasClass("active")) {
                $(tableCheckBoxs).addClass("active");
            } else {
                tableCheckBoxs.removeClass("active");
            }
        }
    });


}

function RadioBox() {
    var radiobox = $('.kod-radiobox label');
    radiobox.click(function () {
        if (!$(this).hasClass("active")) {
            $('.kod-radiobox label').removeClass("active");
            $(this).addClass("active");
        }
    });
}

function Tooltip() {
    $('[data-toggle="tooltip"]').tooltip();
    $('[data-toggle="tooltip"]').click(function () {
        $(this).tooltip('hide');
    });
}

function Loading() {
    KodLoading.open();
    setTimeout(function () {
        KodLoading.close();
    }, 1000);
}

function MenuSelect(li) {
    $('#' + li + ' > a').addClass("active");
    $('#' + li + ' > ul').show();
}

function EnterDisabled() {
    $('body').on('keyup keypress', function (e) {
        var keyCode = e.keyCode || e.which;
        if (keyCode === 13) {
            e.preventDefault();
            return false;
        }
    });
}

function EnterTrigger(Input, Button, afterFunction) {
    $(Input).keypress(function (e) {
        console.log(e.which);
        if (e.which === 13) {
            $(Button).trigger('click');
            afterFunction();
        }
    });
}

function Show_Hide_Event() {
    $.each(['show', 'hide'], function (i, ev) {
        var el = $.fn[ev];
        $.fn[ev] = function () {
            this.trigger(ev);
            return el.apply(this, arguments);
        };
    });
}
 
 

function ExchangeAutoUpdate(button) {
    KodLoading.open();
    $.ajax({
        type: 'post',
        url: '/tool/ExchangeAutoUpdate',
        success: function (data) {
            if (data) {
                setInterval(function () { KodModal.pageRefresh(); }, 1000);
            }
            KodLoading.close();
        }
    });
}

function GoWeb() {
    window.open(getDomain(), '_blank');
}

function ClearCache(button) {
    var newUrl = getDomain() + "/cache/clear-all";
    window.open(newUrl, '_blank');
}

function getDomain() {
    var full = window.location.host;
    var parts = full.split('.');
    var sub = parts[0];
    var domain = parts[1];
    var type = parts[2];
    if (parts.length > 3) {
        type = type + "." + parts[3];
    }
    var newUrl = "http://" + domain + "." + type;
    return newUrl;
}

function TextareaLength() {
    $("textarea[maxlength]").bind('input propertychange', function () {
        var maxLength = $(this).attr('maxlength');
        if ($(this).val().length > maxLength) {
            $(this).val($(this).val().substring(0, maxLength));

        } else {
            var div = $(this).parents('.textareaLength');
            $('span', div).text($(this).val().length);
        }
    })
}
var Filter = {

    Page: function (page) {
        var params = Filter.querystring.add("PageNumber", page, true);
        Filter.querystring.redirect(params);
    },

    Clear: function () {
        let querystring = Filter.querystring.read();
        KodLoading.refresh(window.location.pathname + "?keyword=" + querystring.get('keyword'));
    },

    querystring: {
        read: function () {
            return new URLSearchParams(window.location.search);
        },
        add: function (key, value, isChange) {
            var urlParams = Filter.querystring.read();
            if (isChange) {
                urlParams.delete(key);
                urlParams.append(key, value);
            } else {
                var newParams = new URLSearchParams();

                var control = false;

                for (let pair of urlParams.entries()) {
                    if (pair[0] == key & pair[1] == value) {
                        control = true;
                    } else {
                        newParams.append(pair[0], pair[1]);
                    }
                }
                if (!control) {
                    newParams.append(key, value);
                }
                urlParams = newParams;
            }
            return urlParams;
        },
        redirect: function (newParams) { 
            var url = decodeURIComponent(newParams.toString());
            if (url.indexOf('%') > -1) {
                url = url.replace('%', '%25');
            } 

            KodLoading.refresh(window.location.pathname + "?" + url);
        }
    },

    Search: function (button) {
        //Formu buluyoruz. 
        var form = $(button).parents('form');
        //Mevcut url okuyoruz.
        var url = window.location.pathname;
        //İlk parametreyi bulabilmek için değişken tanımlıyoruz.
        var first = true;
        //Formun içindeki inputlarda dönüyoruz. 
        $('input', form).each(function (index, item) {
            //İnputun name alanını alıyoruz.
            var key = $(item).attr('name');
            //İnputun değerini alıyoruz.
            var value = $(item).val();
            //İnput dolumu kontrol ediyoruz.
            if (!(!value)) {
                //İlk parametre ise ? işaretini değilse & işaretini url ekliyoruz. 
                var _icon = first ? "?" : "&";
                url += _icon + key + "=" + value;
                first = false;
            }

        });
        //Formun içindeki param etiketli alanlarda dönüyoruz.
        $('.param.active', form).each(function (index, item) {
            //name alanını alıyoruz.
            var key = $(item).attr('name');
            //değerini alıyoruz.
            var value = $(item).data('id');
            //dolumu kontrol ediyoruz.
            if (!(!value) || value == 0) {
                //İlk parametre ise ? işaretini değilse & işaretini url ekliyoruz. 
                var _icon = first ? "?" : "&";
                url += _icon + key + "=" + value;
                first = false;
            }

        });

        if (url.indexOf('%') > -1) {
            url = url.replace('%', '%25');
        }
        //Yapılandırdığımız urlye gönderiyoruz.
        KodLoading.refresh(url);
    },

}
var Tool = {
    refresh: function (url) {
        if (url == undefined) {
            url = $(location).attr('href');
        }
        window.location.href = url;
    }
}

var parse = {
    float: function (value, decimal) {

        //Parametrenin virgülden sonraki basamak sayısı verilmediyse 2 olarak tanımlıyoruz.
        if (decimal == undefined) {
            decimal = 2;
        }

        //Parametrenin tipi string değilse çevriliyor.
        if ($.type(value) != "string") {
            value = value.toString();
        }

        //Parametre içindeki virgüller nokta ile değiştiriliyor.
        if (value.indexOf(',') > -1) {
            value = value.replace(",", ".");
        }

        //Null kontrolü yapılıyor. Null ise 0 ile değiştiriliyor.
        value = isNaN(value) ? 0 : value;

        //Parametre parse edilip float tipine çevriliyor.
        return this.fixed(value, decimal);
    },
    int: function (value) {
        //Parametrenin tipi string değilse çevriliyor.
        if ($.type(value) != "string") {
            value = value.toString();
        }

        //Parametre içindeki virgüller nokta ile değiştiriliyor.
        if (value.indexOf(',') > -1) {
            value = value.replace(",", ".");
        }

        //Parametre parse edilip int tipine çevriliyor.
        value = parseInt(value);

        //Null kontrolü yapılıyor. Null ise 0 ile değiştiriliyor.
        value = isNaN(value) ? 0 : value;

        return this.fixed(value, 0);
    },
    fixed: function (value, decimal) {
        return parseFloat(value).toFixed(decimal);
    },
    isNaN: function (value) {
        return isNaN(value) ? 0 : value;
    },
    show: function (value) {

        //Null kontrolü yapılıyor. Null ise 0 ile değiştiriliyor.
        value = isNaN(value) ? "0,00" : value;

        //Geri döndürülmeden önce noktalar virgül ile değiştiriliyor.
        return value.toString().replace('.', ',');
    },
    date: function (datetime) {

        var result = [];
        var d = datetime == undefined ? new Date() : new Date(datetime);

        var day = d.getDate();
        day = (day < 10) ? ("0" + day.toString()) : day.toString();
        result.push({ key: "day", value: day });

        var month = d.getMonth() + 1;
        month = (month < 10) ? ("0" + month.toString()) : month.toString();
        result.push({ key: "month", value: month });

        var year = d.getFullYear().toString();
        result.push({ key: "year", value: year });

        var hour = d.getHours();
        hour = (hour < 10) ? ("0" + hour.toString()) : hour.toString();
        result.push({ key: "hour", value: hour });

        var minute = d.getMinutes();
        minute = (minute < 10) ? ("0" + minute.toString()) : minute.toString();
        result.push({ key: "minute", value: minute });

        var second = d.getSeconds();
        second = (second < 10) ? ("0" + second.toString()) : second.toString();
        result.push({ key: "second", value: second });

        return result;
    }
}

var math = {
    plus: function (arr) {
        var result = 0;
        arr.forEach((item, index) => {
            result = parseFloat(result) + parseFloat(item);
        });

        return parse.float(result);
    }
}

var KodTab = {
    Load: function (TabId) {
        var TabHeader = $('#' + TabId + '.widget-tab .widget-tab-header');
        var TabBody = $('#' + TabId + '.widget-tab .widget-tab-body');
        $('.widget-tab-page', TabBody).removeClass("active");
        $('li a', TabHeader).removeClass("active");

        $('li:first-child a', TabHeader).addClass("active");
        $('.widget-tab-page:first-child', TabBody).addClass("active");

        $('li a', TabHeader).click(function () {
            $('li a', TabHeader).removeClass("active");
            $('.widget-tab-page', TabBody).removeClass("active");
            $(this).addClass("active");
            $('.widget-tab-page#' + $(this).data("tabpage"), TabBody).addClass("active");
        });
    },

}

var KodNumber = {
    Load: function () {
        $('.kod-number').keypress(function (e) {
            if ((event.which != 44 || $(this).val().indexOf(',') != -1) && (event.which < 48 || event.which > 57)) {
                event.preventDefault();
            }
        });

        $('.kod-number').focusout(function () {
            var s = $(this).kodMoney();
            $(this).val(s);
        });
    }
}

var KodValidate = {
    Load: function (isValid) {
        if (!isValid) {
            $("html, body").stop().animate({ scrollTop: 0 }, '500', 'swing');
            $('.kod-validation-summary').show();
        }
    },
    close: function () {
        $('.kod-validation-summary').hide();
    }
}

var KodThumb = {
    Load: function (option) {
        var thumb = $(option.thumb);
        thumb.imgLiquid({
            fill: true,
            horizontalAlign: "center",
            verticalAlign: "top"
        });
    }
}

var KodLoading = {
    open: function () { $('.kod-loading').fadeIn("fast"); $('.kod-loading .percent').remove(); },
    close: function () { $('.kod-loading').fadeOut("fast"); },
    progress: function (percent) {
        if ($('.kod-loading .percent').is(':visible')) {
            $('.kod-loading .percent').html(percent);
        } else {
            $('.kod-loading').append('<div class="percent">' + percent + '</div>');
        }
    },
    refresh: function (url) {
        if (url == undefined || url == null) {
            location.reload();
        } else {
            window.location.href = url;
        }
    }
}

var KodJson = {
    remove: function (array, property, value) {
        var i, j, cur;
        for (i = array.length - 1; i >= 0; i--) {
            cur = array[i];
            if (cur[property] === value) {
                array.splice(i, 1);
            }
        }
    },
    get: function (array, property, value) {
        var i, j, cur;
        for (i = array.length - 1; i >= 0; i--) {
            cur = array[i];
            if (cur[property] === value) {
                return cur;
            }
        }
    }
}

var isNull = {
    control: function (value) {
        if (value == null) {

            return '';
        }
        return value;
    }
}

var Translate = {
    Modal: function (TranslateTable, TableId) {
        var modal = $('#LanguageTableModal');
        KodTab.Load("LanguageTab");
        $('#TranslateTable', modal).val(TranslateTable);
        $('#TableId', modal).val(TableId);
        $("input[type='text'], textarea", ".widget-tab-page").val("");

        //Çeviri Tipi 1 ise Sayfa içeriği demek
        if (TranslateTable == 1) {
            $('.slider', modal).hide();
            //Çeviri Tipi 2 ise Slider içeriği demek
        } else if (TranslateTable == 2) {
            $('.slider', modal).show();
            $('.ckeditor', modal).hide();
        }

        var isClear = true;
        $.ajax({
            type: 'post',
            url: '/settings/language/table/get',
            async: false,
            data: { TranslateTable, TableId },
            success: function (data) {
                data.forEach(function (item, index) {

                    var div = $('#Tab-' + item.LanguageId);
                    $("#Title-" + item.LanguageId, div).val(item.Title);
                    $("#HeaderTitle-" + item.LanguageId, div).val(item.HeaderTitle);
                    $("#ButtonText-" + item.LanguageId, div).val(item.ButtonText);
                    CKEDITOR.instances['Description-' + item.LanguageId].setData(item.Description);
                    $("#Description-" + item.LanguageId, div).val(item.Description);
                    isClear = false;
                });
            }
        });
        if (isClear) {
            for (instance in CKEDITOR.instances) {
                CKEDITOR.instances[instance].setData("");
            }
        }

        KodModal.open('#LanguageTableModal');
    },
    Save: function (button) {
        var modal = $('#LanguageTableModal');
        var tab = $(button).parents('.widget-tab-page.active');
        var TranslateTable = $('#TranslateTable', modal).val();
        var Description = $('#Description-' + $('#LanguageId', tab).val(), tab).val();
        if (TranslateTable == 1) {
            Description = CKEDITOR.instances['Description-' + $('#LanguageId', tab).val()].getData()
        }
        KodLoading.open();
        var entity = {
            TranslateTable: TranslateTable,
            TableId: $('#TableId', modal).val(),
            LanguageId: $('#LanguageId', tab).val(),
            Title: $('#Title-' + $('#LanguageId', tab).val(), tab).val(),
            HeaderTitle: $('#HeaderTitle-' + $('#LanguageId', tab).val(), tab).val(),
            ButtonText: $('#ButtonText-' + $('#LanguageId', tab).val(), tab).val(),
            Description: Description
        }

        var data = new FormData();
        data.append("TranslateTable", entity.TranslateTable);
        data.append("TableId", entity.TableId);
        data.append("LanguageId", entity.LanguageId);
        data.append("Title", entity.Title);
        data.append("HeaderTitle", entity.HeaderTitle);
        data.append("ButtonText", entity.ButtonText);
        data.append("Description", entity.Description);

        if (entity.Title != "") {
            $.ajax({
                type: 'post',
                url: '/settings/language/table/save',
                dataType: 'json',
                processData: false, contentType: false,
                data: data,
                success: function (data) {
                    if (data) {
                        KodModal.message.show({
                            type: 'success',
                            title: 'Tebrikler!',
                            text: 'Kayıt başarıyla güncellenmiştir.'
                        });
                    } else {
                        KodModal.message.show({
                            type: 'warning',
                            title: 'Uyarı!',
                            text: 'Bir sorun oluştu. Daha sonra tekrar deneyiniz.'
                        });
                    }
                    KodLoading.close();
                }
            });
        } else {
            KodModal.message.show({
                type: 'warning',
                title: 'Uyarı!',
                text: 'Başlık alanı boş bırakılamaz.'
            });
            KodLoading.close();
        }

    },
    Preview: function (input) {
        readURLPreview(input);
    }
}

function readURLPreview(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('.image-preview img').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    } else {
        $('.image-preview img').attr('src', '');
    }
}

var SupportRequest = {
    Modal: function (button) {
        var modal = $('#TabSupportRequestDetailModal');
        var box = $('> a', $(button).parents('.comment-item:first'));
        var id = $(box).data('id');
        var name = $(box).data('name');
        var mail = $(box).data('mail');
        var phone = $(box).data('phone');
        var date = $(box).data('date');
        $('#request_detail_id', modal).val(id);
        $('#request_detail_name', modal).text(name);
        $('#request_detail_mail', modal).text(mail);
        $('#request_detail_phone', modal).text(phone);
        $('#request_detail_date', modal).text(date);
        KodModal.open("#TabSupportRequestDetailModal", "Destek Talepleri");
    },
    Remove: function (button) {
        var modal = $(button).parents('#TabSupportRequestDetailModal');
        var SupportRequestId = $('#request_detail_id', modal).val();
        KodModal.alert.remove({
            removedName: "destek talebi",
            callback: function () {
                $.ajax({
                    type: 'post',
                    url: '/support-request/remove',
                    data: { SupportRequestId },
                    success: function (data) {
                        if (data) {
                            Tool.refresh();
                        } else {
                            KodModal.message.show({
                                type: 'warning',
                                title: 'Uyarı!',
                                text: 'Destek talebi silinememiştir.'
                            });
                        }
                    }
                });
            }
        });
    },
    Add: {
        Phone: function (button) {
            var modal = $(button).parents('#TabSupportRequestDetailModal');
            var Name = $('#request_detail_name', modal).text();
            var Phone = $('#request_detail_phone', modal).text();
            $.ajax({
                type: 'post',
                url: '/content/phone-list/add',
                data: { Name, Phone },
                success: function (data) {
                    if (data) {
                        KodModal.message.show({
                            type: 'info',
                            title: 'Tebrikler!',
                            text: 'Telefon numarası Listeye eklenmiştir.'
                        });
                    } else {
                        KodModal.message.show({
                            type: 'warning',
                            title: 'Uyarı!',
                            text: 'Telefon numarası listenizde mevcuttur.'
                        });
                    }
                }
            });
        },
        Mail: function (button) {
            var modal = $(button).parents('#TabSupportRequestDetailModal');
            var Name = $('#request_detail_name', modal).text();
            var Email = $('#request_detail_mail', modal).text();
            $.ajax({
                type: 'post',
                url: '/content/mailing-list/add',
                data: { Name, Email },
                success: function (data) {
                    if (data) {
                        KodModal.message.show({
                            type: 'info',
                            title: 'Tebrikler!',
                            text: 'Mail adresi listeye eklenmiştir.'
                        });
                    } else {
                        KodModal.message.show({
                            type: 'warning',
                            title: 'Uyarı!',
                            text: 'Mail adresi listenizde mevcuttur.'
                        });
                    }
                }
            });
        }
    }
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}