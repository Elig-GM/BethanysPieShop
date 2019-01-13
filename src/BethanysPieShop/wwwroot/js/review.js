$("#review").on("click", function () {
    $.ajax({
            method: 'PUT',
            url: '/api/PieData/'+document.getElementById('id_Pie').value,
            contentType: "application/json",
            dataType: 'json',
            data: JSON.stringify(document.getElementById('rev').value),
            success: function (jsonData) {
                if (jsonData == null) {
                    alert('no data returned');
                    return;
                }
                debugger;
                $.each(jsonData.value, function (index, pie) {

                    alert("ciclo");
                });
            },
            error: function (ex) {
                alert(ex);
            }
    });
});