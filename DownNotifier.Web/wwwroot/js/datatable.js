$(function () {
    KodTable.Load();
});


function TableCheckBox() {
    var checkbox = $('table.table .kod-checkbox label');
    checkbox.click(function () {
        var checkcount = $('table.table td .kod-checkbox label.active').length;
        $('.ButtonselectedRemove').hide();
        if (checkcount > 0) {
            var title = checkcount === 1 ? "Seçileni Sil" : "Seçilenleri Sil";
            $('.ButtonselectedRemove').show();
            $('.ButtonselectedRemove button').attr("data-original-title", title);
        }
    });
}

function TableButton() {
    //Detaylı Arama butonuna tıklanınca yapılacak işlemler
    $('.button-search').click(function () {
        $('.widget-detail-search').slideToggle();
        $('.widget-search .input-search').fadeToggle();
    });

    //YAZDIR butonuna tıklanınca yapılacak işlemler
    $('.button-print').click(function () {
        alert('yazdır');
    });

    //PDF butonuna tıklanınca yapılacak işlemler
    $('.button-pdf').click(function () {
        var pdfName = "";
        PdfDownload({
            pdfname: 'deneme',
            title: 'Test Sayfası',
            line: '', //headerLineOnly, lightHorizontalLines, noBorders

        });
    });
}

function SelectedIds() {
    var IdArray = [];
    $('table.table tbody .kod-checkbox label.active').each(function (index, item) {
        var Id = $('input[type="hidden"]', $(item).parent().parent()).val();
        IdArray.push(Id);
    });
    return IdArray;
}

function PdfDownload(settings) {

    var PdfContent = {
        content: [
            { text: settings.title, fontSize: 15, bold: true, margin: [0, 0, 0, 5] },
            {
                style: 'table',
                table: {
                    headerRows: 1,
                    widths: ['*', '*', '*'],
                    body: [
                        [{ text: 'Header 1', style: 'header' }, { text: 'Header 2', style: 'header' }, { text: 'Header 3', style: 'header' }],
                        ['Sample value 1', 'Sample value 2', 'Sample value 3'],
                        ['Sample value 1', 'Sample value 2', 'Sample value 3'],
                        ['Sample value 1', 'Sample value 2', 'Sample value 3'],
                        ['Sample value 1', 'Sample value 2', 'Sample value 3'],
                        ['Sample value 1', 'Sample value 2', 'Sample value 3'],
                    ]
                },
                layout: settings.line
            }
        ],
        styles: {

            header: {
                fontSize: 12,
                bold: true,
                margin: [0, 0, 0, 0]
            },
            table: {
                fontSize: 11,
                margin: [0, 0, 0, 0]
            }
        },
    };
    var pdf = pdfMake.createPdf(PdfContent);
    pdf.print();
    //pdf.download(settings.pdfname + '.pdf');
}

function PageSizeSelect() {
    $('#PageSize').val($('#PageSize').attr("value"));
  
}

var KodTable = {
    Load: function () {
        CheckBoxAll();
        TableButton();
        TableCheckBox();
        PageSizeSelect();
        KodTable.form.parameterClear();
        KodTable.KeywordControl();
        KodTable.isSearchPanel();
    },
    form: {
        submit: function () {
            $("form").submit(function () {
                $(this).find(":input").filter(function () { return !this.value; }).attr("disabled", "disabled");
                $('input[name*="item."]', this).attr("disabled", "disabled")
                return true;
            });
        },
        parameterClear: function () {
            $("form").find(":input").prop("disabled", false);
            $('input[name*="item."]', 'form').prop("disabled", false);
        }
    },
    DetailSearhButton: function (button) {
        $('#isSearch').val("true");
        $('#Keyword').val("");
        KodLoading.open();
        KodTable.form.submit();
        return true;
    },
    DetailSearchClear: function () {
        $('#isSearch').val("false");
        return true;
    },
    SearhButton: function (button) {
        KodLoading.open();
        KodTable.form.submit();
        return true;
    },
    SearchClear: function () {
        $('#Keyword').val("");
        KodLoading.open();
        KodTable.form.submit();
        return true;
    },
    KeywordControl: function () {
        if ($('#Keyword').val() == "") {
            $('#KeywordClearButton').hide();
        } else {
            $('#KeywordClearButton').show();
        }
    },
    PageSizeChange: function (select) {
        var form = $(select).parents('form:first');
        KodTable.SearhButton(select);
        form.submit();

        console.log($('#PageSize').val());
    },
    isSearchPanel: function () {
        var isSearch = $('.widget-detail-search').data('issearch');
        if (isSearch == "True") {
            $('.widget-detail-search').show();
            $('.widget-search .input-search').hide();
        }
    }
}