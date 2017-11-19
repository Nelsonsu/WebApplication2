function checkuser() {
    var splitter = document.cookie.split(";");
    var user = splitter[0];
    if (user !== "nouser") {
        $('#userlinks').val(user);
        alert(user);
    } else {
        alert("You are not signed in.");
        var url = "Login";
        window.location.href = url;
    }
}