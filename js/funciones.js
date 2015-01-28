function pnotifySuccess(t, msg, ty) {
    $(function () {
        $.pnotify({
            title: t,
            text: msg,
            type: ty,
            history: false,
            width: "60%",
            delay: 500,
            sticker: false,
            shadow: true,
            addclass: "stack-bar-bottom",
            stack: false,
        });
    });
}

$(window).load(function () {
    $('.loading').fadeToggle(100);
});

function checkAll(objRef) {
    var table = objRef.parentNode.parentNode.parentNode.parentNode;
    var inputList = table.getElementsByTagName("input");
    for (var i = 0; i < inputList.length; i++) {
        var row = inputList[i].parentNode.parentNode;
        if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
            var id_tabla = $(table).attr('id');
            if (objRef.checked) {
                $('#' + id_tabla + ' tr').css("background-color", "#f5f5f5");
                inputList[i].checked = true;
            }
            else {
                $('#' + id_tabla + ' tr').css("background-color", "");
                inputList[i].checked = false;
            }
        }
    }
}

function Check_Click(objRef) {
    var row = objRef.parentNode.parentNode;
    if (objRef.checked) {
        row.style.backgroundColor = "#f5f5f5";
    }
    else {
        row.style.backgroundColor = "";
    }
    var GridView = row.parentNode;
    var inputList = GridView.getElementsByTagName("input");
    for (var i = 0; i < inputList.length; i++) {
        var headerCheckBox = inputList[0];
        var checked = true;
        if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
            if (!inputList[i].checked) {
                checked = false;
                break;
            }
        }
    }
    headerCheckBox.checked = checked;
    if (checked == true) {
        headerCheckBox.parentNode.parentNode.style.backgroundColor = "#f5f5f5";
    } else {
        headerCheckBox.parentNode.parentNode.style.backgroundColor = "";
    }
}

$(' :text').keydown(function (evt) {
    var code = evt.charCode || evt.keyCode;
    if (code == 27) {
        $(this).val('');
    }
});

function getValidar() {
    var resultado = true;

    var campoObligatorio = '<div class="{0} text-error">Campo obligatorio</div>';
    $('.obligatorio').each(function () {
        $('.' + $(this).attr('id')).remove();
        if ($(this)[0].tagName.toUpperCase() == 'INPUT') {
            if ($.trim($(this).val()) == '') {
                resultado = false;
                campoObligatorio = campoObligatorio.replace("{0}", $(this).attr('id'))
                $(this).parent().append(campoObligatorio);
            }
        } else if ($(this)[0].tagName.toUpperCase() == 'SELECT') {
            if ($.trim($(this).val()) == 0) {
                resultado = false;
                campoObligatorio = campoObligatorio.replace("{0}", $(this).attr('id'))
                $(this).parent().append(campoObligatorio);
            }
        } else if ($(this)[0].tagName.toUpperCase() == 'TEXTAREA') {
            if ($.trim($(this).val()) == 0) {
                resultado = false;
                campoObligatorio = campoObligatorio.replace("{0}", $(this).attr('id'))
                $(this).parent().append(campoObligatorio);
            }
        }
    });

    var campoNumerico = '<div class="{0} text-error">Solo numeros</div>';
    var campoEntero = '<div class="{0} text-error">Solo entero</div>';
    var campoDecimal = '<div class="{0} text-error">Solo Decimal</div>';
    var resultadoNumero = true;
    $('.numerico').each(function () {
        $('.' + $(this).attr('id')).remove();
        if (isNaN($(this).val())) {
            campoNumerico = campoNumerico.replace("{0}", $(this).attr('id'))
            $(this).parent().append(campoNumerico);
            resultadoNumero = false;
        } else {
            if ($(this).hasClass('entero')) {
                $('.' + $(this).attr('id')).remove();
                if ($(this).val() % 1 != 0 && $.trim($(this).val()) != '') {
                    campoEntero = campoEntero.replace("{0}", $(this).attr('id'))
                    $(this).parent().append(campoEntero);
                    resultadoNumero = false;

                }
            }
            if ($(this).hasClass('decimal')) {
                $('.' + $(this).attr('id')).remove();
                if ($(this).val() % 1 == 0 && $.trim($(this).val()) != '') {
                    campoDecimal = campoDecimal.replace("{0}", $(this).attr('id'))
                    $(this).parent().append(campoDecimal);
                    resultadoNumero = false;

                }
            }
        }
    });

    var resultadoTexto = true;
    var CampoTexto = '<div class="{0} error">Solo Texto</div>';

    $('.texto').each(function () {
        $('.' + $(this).attr('id')).remove();
        if (isNaN($(this).val()) == false) {
            CampoTexto = CampoTexto.replace("{0}", $(this).attr('id'))
            $(this).parent().append(CampoTexto);
            resultadoTexto = false;
        }

    });

    if (resultado == true && confirm("¿Está seguro(a) que todos los datos ingresados son correctos?") == false) resultado = false;
    return resultado && resultadoNumero && resultadoTexto;
}