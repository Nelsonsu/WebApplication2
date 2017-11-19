//adult count
var acount = 1;
var countAl = document.getElementById("adultcount");

function adultplus() {
    acount++;
    countAl.value = acount;
}
function adultminus() {
    if (acount > 1) {
        acount--;
        countAl.value = acount;
    }
}

//child count

var ccount = 1;
var countEl = document.getElementById('childcount');
function childplus() {
    ccount++;
    countEl.value = ccount;
}
function childminus() {
    if (ccount > 0) {
        ccount--;
        countEl.value = ccount;
    }
}

//show roundtrip
function roundtripshow() {
    $("#multicity").hide();
    $("#roundtrip").show();
}

//show one way
function onewayshow() {
    document.getElementById('roundtrip').style.display = 'none';
    document.getElementById('multicity').style.display = 'none';
}
//show multicity
function multicityshow() {
    document.getElementById('roundtrip').style.display = 'none';
    document.getElementById('multicity').style.display = 'block';
}

function checkuser() {
    var splitter = document.cookie.split(";");
    var user = splitter[0];
    if (user !== "nouser") {
        $('#logoutlinks').show();
        $('#userlinks').text(user);
        $('#useremail').text(user);
        $('#userlinks').show();
        $('#registerlinks').hide();
        $('#loginlinks').hide();
    } else {
        $('#logoutlinks').hide();
        $('#userlinks').hide();
        $('#registerlinks').show();
        $('#loginlinks').show();
    }
}

function logout() {
    document.cookie = "nouser"+";expires=Fri, 13 Mar 2020 12:00:00 UTC;path=/";
    location.reload();
    $('#registerlinks').show();
    $('#loginlinks').show();
}

function viewdetail() {
    var splitter = document.cookie.split(";");
    var user = splitter[0];
    window.location.href = "/Home/ViewMyFlight?email="+user;
}

