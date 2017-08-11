function erroAlert(mensagemDeErro) {
    $.notify({
        message: mensagemDeErro
    }, {
            placement: {
                from: "top",
                align: "center"
            },
            type: 'danger'
    });
}

function successAlert(mensagemDeSucesso) {
    $.notify({
        message: mensagemDeSucesso
    }, {
            placement: {
                from: "top",
                align: "center"
            },
            type: 'success'
        });
}