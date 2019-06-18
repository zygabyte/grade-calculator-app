$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    $("#signInForm").submit(function(e){
        e.preventDefault();
    });
}

function signIn() {
    const matric = $('#matric').val();
    const password = $('#password').val();
    
    console.log('cred', matric, " password", password);
}

