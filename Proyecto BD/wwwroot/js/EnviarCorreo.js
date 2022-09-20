$(document).ready(function () {
    $('#Enviar').click(function (e) {
        e.preventDefault();
        let model = $("#correo").val();
        let nombre = $("#Nombre").val();
        let telefono = $("#Telefono").val();
        let evento = $("#Evento").val();
        var datos = [model, nombre, telefono, evento];
        
        if (ValidarCorreo(datos[0])) {
            $.ajax({
                type: "POST",
                url: "/Home/Recuperar",
                data: {
                    datos
                },
                dataType: "json",
                success: function (response) {
                    console.log(response);
                    console.log(datos);
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