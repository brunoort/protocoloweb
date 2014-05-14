function validate(evt) {
    var theEvent = evt || window.event;
    var key = (theEvent.which) ? theEvent.which : theEvent.keyCode
    key = String.fromCharCode(key);
    var regex = /[0-9]|\,/;
    if ([evt.keyCode || evt.which] == 8) //this is to allow backspace
        return true;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        theEvent.preventDefault();
    }
}

function atualizaPagina() {
    window.location.reload();
}

function SelecionaTodosChecks() {

    var vCheck;
    if (document.getElementById("GridView1_ctl02_CheckBoxAprova").checked == true) {
        vCheck = false;
    } else {
        vCheck = true;
    }

    for (i = 2; i < 999; i++) {
        if (i < 10) {
            vnum = '0' + i;
        }
        else {
            vnum = i;
        }
        document.getElementById("GridView1_ctl" + vnum + "_CheckBoxAprova").checked = vCheck;
    }
}

function showLinks() {
    document.getElementById("#divLinks").style.display = "inline";
}

function hidePopup() {
    document.getElementById("pnlJustificativas").style.display = "none";
    document.getElementById("popJustificativas_backgroundElement").style.display = "none";

    document.getElementById("pnlSalvaJustificativa").style.display = "none";
    document.getElementById("popSalvaJustificativa_backgroundElement").style.display = "none";

    document.getElementById("pnlCarimbo").style.display = "none";
    document.getElementById("popCarimbo_backgroundElement").style.display = "none";

    document.getElementById("pnlEditar").style.display = "none";
    document.getElementById("popEditar_backgroundElement").style.display = "none";
}

function calculaLiquido() {
    var v1 = document.getElementById("txtIRvalor_edit").value;
    var v2 = document.getElementById("txtPCCvalor_edit").value;
    var v3 = document.getElementById("txtISSvalor_edit").value;
    var v4 = document.getElementById("txtINSSvalor_edit").value;
    var vDesconto = parseFloat(v1) + parseFloat(v2) + parseFloat(v3) + parseFloat(v4);

    var vValorNF = document.getElementById("txtValorNF_carimbo").value;
    var totLiquido = parseFloat(vValorNF - vDesconto);

    //alert("total" + vValorNF - vDesconto);

    document.getElementById("txtLiquido_edit").value = totLiquido.toFixed(2);
}

function calculaPorcentagem(pInputPct, pVlPct, pResultInput) {
    var vValorIni = document.getElementById(pInputPct).value;
    var vValor = vValorIni.replace(",", "");
    var vValorFinal = vValor.replace(",", "");

    if (vValor != "") {
        var valorCalculado = (vValorFinal * parseFloat(pVlPct.replace(',', '.'))) / 100;
        //alert(valorCalculado);

        document.getElementById(pResultInput).value = parseFloat(valorCalculado).toFixed(2);
        calculaLiquido();
    }
}

Number.prototype.formatMoney = function (c, d, t) {
    var n = this, c = isNaN(c = Math.abs(c)) ? 2 : c, d = d == undefined ? "," : d, t = t == undefined ? "." : t, s = n < 0 ? "-" : "", i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "", j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

function calculaBase(pBaseValor, pPct, pResultInput) {
    var vValorIni = document.getElementById(pPct).value;
    var vValor = vValorIni.replace(",", ".");
    //var vValorFinal = vValor.replace(",", "");

    if (pBaseValor != "") {
        var valorCalculado = (pBaseValor.replace(',', '.') * vValor) / 100;
        //alert(pBaseValor + " * " + vValor + " / 100");

        document.getElementById(pResultInput).value = valorCalculado;
        calculaLiquido();
    }
}


function editarFormulario(pIdFormulario, pMatriz) {
    window.open('editarFormulario.aspx?id=' + pIdFormulario + '&mtz=' + pMatriz, 'Editar Formulário', 'scrollbars=1,toolbar=0,location=0,statusbar=0,menubar=0,width=500,height=400,left=10,top=10');
}
function abreCarimbo(pIdFormulario, pMatriz) {
    window.open('Carimbo.aspx?id=' + pIdFormulario + '&mtz=' + pMatriz, 'Carimbo', 'scrollbars=1,toolbar=0,location=0,statusbar=0,menubar=0,width=500,height=800,left=10,top=10');
}

function showCarimboRJ() {
    document.getElementById("Carimbo1").style.display = "block";
    document.getElementById("Carimbo2").style.display = "block";
    document.getElementById("Carimbo3").style.display = "block";
    document.getElementById("Carimbo4").style.display = "block";
}

function abrePopup(pIdFormulario, pRead) {
    window.open('Justificativas.aspx?idform=' + pIdFormulario + '&read=' + pRead, 'detalhe', 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=0,width=500,height=400,left=10,top=10');
}

function fechaJanela(divClose) {
    alert("Dados Salvos com sucesso.");
    document.getElementById(divClose).style.display = "none";
    window.close();
}

function PesquisaAP() {
    var vNumAp = document.getElementById("txtAP").value;

    jQuery.support.cors = true;

    if ($.browser.msie && parseInt($.browser.version, 10) >= 8 && window.XDomainRequest) {
        var url = 'http://request.rederecord.com.br/Ap/consulta?idAp=' + vNumAp;
        var xdr = new XDomainRequest();
        xdr.open("get", url, true);
        xdr.onload = function () {
            json = 'json = ' + xdr.responseText;
            eval(json);
            $('#txtEmpresa').val(json.NOME_FORN);
            $('#txtCnpj').val(json.CNPJ_FORN);

            /*
            $(document).ready(function () {
                $('#sltMatriz option').each(function () {
                    if ($(this).val() == json.CENTRO_PAGTO) {
                        $(this).attr('selected', true);
                    }
                });
            });
            $("#sltMatriz").change();
            */

            $("#sltMatriz").val(json.CENTRO_PAGTO);
            $('#txtNF').val(json.NUMNOTA_PAGTO);
            $('#txtNumPedido').val(json.NUM_PED);
            var dtVencimento = json.VENC_PAGTO;
            var dtVenc = dtVencimento.substring(0, 10);
            var arrDtVenc = dtVenc.split("-");
            $('#txtDataVencimento').val(arrDtVenc[2] + "/" + arrDtVenc[1] + "/" + arrDtVenc[0]);

            /*
            if (json.VALOR_NOTA.length > 2) {
                var tmp = data.VALOR_NOTA + '';
                tmp = tmp.replace(/([0-9]{2})$/g, ",$1");
                if (tmp.length > 6)
                    tmp = tmp.replace(/([0-9]{3}),([0-9]{2}$)/g, ".$1,$2");
            } else {
                var tmp = json.VALOR_NOTA + ",00";
            }*/
            $('#txtValorNF').val(json.VALOR_NOTA);

            var dtEmissao = json.DATA_DOCTO;
            var dtEmissao2 = dtEmissao.substring(0, 10);
            var arrDtEmissaoDoc = dtEmissao2.split("-");
            $('#txtDataEmissaoDoc').val(arrDtEmissaoDoc[2] + "/" + arrDtEmissaoDoc[1] + "/" + arrDtEmissaoDoc[0]);

            $('#txtDepartamento').val(json.DEPART_PAGTO);

            //document.getElementById("linkEditaAP").innerHTML = "<a href=http://10.10.13.13/sistemas/ap/auto_pagto_editar.asp?cod=" + vNumAp + " target='_blank' ><img alt='Editar AP' src='Images/editar.png' width=25 height=25 style='top:5px'/></a>";
        };
        xdr.send();
    } else {
        $.ajax({
            type: 'GET',
            url: 'http://request.rederecord.com.br/Ap/consulta?idAp=' + vNumAp,
            success: function (data) {
                $('#txtEmpresa').val(data.NOME_FORN);
                $('#txtCnpj').val(data.CNPJ_FORN);

                /*
                $(document).ready(function () {
                    $('#sltMatriz option').each(function () {
                        if ($(this).val() == data.CENTRO_PAGTO) {
                            $(this).attr('selected', true);
                        }
                    });
                });

                $("#sltMatriz").change();
                */

                $("#sltMatriz").val(data.CENTRO_PAGTO);

                $('#txtNF').val(data.NUMNOTA_PAGTO);
                $('#txtNumPedido').val(data.NUM_PED);

                var dtVencimento = data.VENC_PAGTO;
                var dtVenc = dtVencimento.substring(0, 10);
                var arrDtVenc = dtVenc.split("-");
                $('#txtDataVencimento').val(arrDtVenc[2] + "/" + arrDtVenc[1] + "/" + arrDtVenc[0]);

                /*
                if (data.VALOR_NOTA.length > 2) {
                    var tmp = data.VALOR_NOTA + '';
                    tmp = tmp.replace(/([0-9]{2})$/g, ",$1");
                    if (tmp.length > 6)
                        tmp = tmp.replace(/([0-9]{3}),([0-9]{2}$)/g, ".$1,$2");
                } else {
                    var tmp = data.VALOR_NOTA+",00"
                }
                */
                $('#txtValorNF').val(data.VALOR_NOTA);

                var dtEmissao = data.DATA_DOCTO;
                var dtEmissao2 = dtEmissao.substring(0, 10);
                var arrDtEmissaoDoc = dtEmissao2.split("-");
                $('#txtDataEmissaoDoc').val(arrDtEmissaoDoc[2] + "/" + arrDtEmissaoDoc[1] + "/" + arrDtEmissaoDoc[0]);

                $('#txtDepartamento').val(data.DEPART_PAGTO);

                //document.getElementById("linkEditaAP").innerHTML = "<a href=http://10.10.13.13/sistemas/ap/auto_pagto_editar.asp?cod=" + vNumAp + " target='_blank' ><img alt='Editar AP' src='Images/editar.png' width=25 height=25 style='top:5px'/></a>";

            },
            error: function (error) {
                alert("AP inexistente");
            }
        });
    }

}

function formatReal(int) {
    var tmp = int + '';
    tmp = tmp.replace(/([0-9]{2})$/g, ",$1");
    if (tmp.length > 6)
        tmp = tmp.replace(/([0-9]{3}),([0-9]{2}$)/g, ".$1,$2");

    return tmp;
}

function MascaraMoeda(objTextBox, SeparadorMilesimo, SeparadorDecimal, e) {
    var sep = 0;
    var key = '';
    var i = j = 0;
    var len = len2 = 0;
    var strCheck = '0123456789';
    var aux = aux2 = '';
    var whichCode = (window.Event) ? e.which : e.keyCode;
    if (whichCode == 13) return true;
    key = String.fromCharCode(whichCode); // Valor para o código da Chave
    if (strCheck.indexOf(key) == -1) return false; // Chave inválida
    len = objTextBox.value.length;
    for (i = 0; i < len; i++)
        if ((objTextBox.value.charAt(i) != '0') && (objTextBox.value.charAt(i) != SeparadorDecimal)) break;
    aux = '';
    for (; i < len; i++)
        if (strCheck.indexOf(objTextBox.value.charAt(i)) != -1) aux += objTextBox.value.charAt(i);
    aux += key;
    len = aux.length;
    if (len == 0) objTextBox.value = '';
    if (len == 1) objTextBox.value = '0' + SeparadorDecimal + '0' + aux;
    if (len == 2) objTextBox.value = '0' + SeparadorDecimal + aux;
    if (len > 2) {
        aux2 = '';
        for (j = 0, i = len - 3; i >= 0; i--) {
            if (j == 3) {
                aux2 += SeparadorMilesimo;
                j = 0;
            }
            aux2 += aux.charAt(i);
            j++;
        }
        objTextBox.value = '';
        len2 = aux2.length;
        for (i = len2 - 1; i >= 0; i--)
            objTextBox.value += aux2.charAt(i);
        objTextBox.value += SeparadorDecimal + aux.substr(len - 2, len);
    }

    return false;
}

function verificaValor() {
    var objTextBox = document.getElementById("txtValorNF").value;

    if (objTextBox.length > 15) {
        alert("Valor Inválido");
        document.getElementById("txtValorNF").value = "";
    }
}

function validaEmail(email) {
    var str = email;
    var filtro = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;

    if (filtro.test(str)) {
        return true;
    } else {
        alert("E-mail inválido, favor inserir seu e-mail completo.");
        $("#txtEmailResponsavel").val("");
    }
}


function SomenteNumero(e) {
    var tecla = (window.event) ? event.keyCode : e.which;
    if ((tecla > 47 && tecla < 58)) return true;
    else {
        if (tecla == 8 || tecla == 0) return true;
        else return false;
    }
}

function selecionaDocumento(pDocumento) {
    document.getElementById("txtDocumento").value = pDocumento;
    document.getElementById("popupFiles").style.display = "none";
}
