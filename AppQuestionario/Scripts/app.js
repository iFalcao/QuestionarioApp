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