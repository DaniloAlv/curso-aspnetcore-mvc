function AjaxModal() {
    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    $("#myModalContent").load(this.href,
                        function () {
                            $("#myModal").modal({ keyboard: true },
                                'show');
                            bindForm(this);
                        });
                    return false;
                });
        });

        function bindForm(dialog) {
            $('form', dialog).submit(function () {
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function (result) {
                        if (result.success) {
                            $("#myModal").hide();
                            $("#EnderecoTarget").load(result.url);
                        }
                        else {
                            $("#myModalContent").html(result);
                            bindForm(dialog);
                        }
                    }
                });
            });
        }
    });
}

function BuscaCep() {
    $(document).ready(function () {
        
        function limpaFormularioCep() {
            $("#Endereco_Logradouro").val("");
            $("#Endereco_Cidade").val("");
            $("#Endereco_Estado").val("");
            $("#Endereco_Bairro").val("");
        }

        $("#Endereco_CEP").blur(function () {
            var cep = $(this).val().replace(/\D/g, '');

            if (cep != '') {
                var validaCEP = /^[0-9]{8}$/;

                if (validaCEP.test(cep)) {
                    $("#Endereco_Logradouro").val("...");
                    $("#Endereco_Cidade").val("...");
                    $("#Endereco_Estado").val("...");
                    $("#Endereco_Bairro").val("...");

                    //Consulta o serviço viacep.com.br
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                        function (dados) {
                            if (!("erro" in dados)) {
                                $("#Endereco_Logradouro").val(dados.logradouro);
                                $("#Endereco_Cidade").val(dados.localidade);
                                $("#Endereco_Estado").val(dados.uf);
                                $("#Endereco_Bairro").val(dados.bairro);
                            }
                            else {
                                limpaFormularioCep();
                                alert("CEP não encontrado!");
                            }
                        });
                }
                else {
                    limpaFormularioCep();
                    alert("Formato de CEP inválido!");
                }
            }
            else {
                limpaFormularioCep();
            }
        });
    });
}

$(document).ready(function () {
    $("#msg_box").fadeOut(4000);
});
