window.addEventListener("load", function () {
    var a = document.querySelector("a.PostLogoutRedirectUri");
    if (a) {
        alert(a);
        window.location = a.href;
    }
});
