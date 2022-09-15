$(document).ready(function () {
    $('#Enviar').click(function (e) {
        e.preventDefault();
        let model = $("#correo").val();
        console.log(model)
        if (ValidarCorreo(model)) {
            $.ajax({
                type: "POST",
                url: "/Home/Recuperar",
                data: {
                    model
                },
                dataType: "json",
                success: function (response) {
                    console.log(response);
                    console.log(model);
                    if (response === 1) {
                        swal("", "Correo enviado exitosamente", "success");                        
                    } else {
                        swal("Error", "El correo introducido no es valido", "warning");                        
                    }
                }
            });
        } else {
            swal("Error", "Por favor llenar el campo respectivo", "warning");            
        }
    });
});

function ValidarCorreo(correo) {
    if (correo != "") {
        return true;
    }
    return false;
}