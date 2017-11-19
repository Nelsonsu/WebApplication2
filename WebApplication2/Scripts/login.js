
function loginact() {
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '/Home/Loginaction',
        data: { Email: $('#emailaddressinput').val(), Password: $('#passwordinput').val() },
        success: function (data) {
            if (data.id === "User Does not Exists") {
                $(".errors").show();
            } else {
                document.cookie = data.id + ";expires=Fri, 13 Mar 2020 12:00:00 UTC;path=/";
                var url = "Homepage";
                window.location.href = url;
            }
        }
        
    });
};