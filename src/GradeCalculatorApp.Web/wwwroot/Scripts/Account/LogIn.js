$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    $("#signInForm").submit(function(e){
        e.preventDefault();
    });
}

function signIn() {
    $('#signInButton').attr('disabled', true);
    
    const email = $('#email').val();
    const password = $('#password').val();
    const userRole = $('#userRole').val();

    api("POST",
        "/Account/LogInUser",
        {email: email, password: password, userRole: userRole},
        true,
        logInResponse, true, true);
}

function logInResponse(data) {
    if (data.status){
        toastr.success('Successfully signed in', 'Success');
        setTimeout(() => window.location = '/', 2000 );
        return;
    }

    $('#signInButton').attr('disabled', false);
}