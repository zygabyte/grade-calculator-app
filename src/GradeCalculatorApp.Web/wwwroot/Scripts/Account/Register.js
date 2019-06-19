$(document).ready(function () {
    pageLoad();
});

function pageLoad() {
    $("#registerForm").submit(function(e){
        e.preventDefault();
    });
}


function register(){
    const firstname = $('#firstname').val();
    const lastname = $('#lastname').val();
    const email = $('#email').val();
    const password = $('#password').val();
    const confirm = $('#confirm_password').val();
    
    if (password !== confirm){
        toastr.error('Passwords do not match. Please try again', 'Error');
        return;
    }
    
    let user = {
        FirstName: firstname,
        LastName: lastname,
        Email: email, 
        UserRole: userType,  
        PasswordHash: password 
    };

    api("POST",
        "/Account/RegisterUser",
        {user: user},
        true,
        registerUserResponse, true);
}

function registerUserResponse(data) {
    if (data.status){
        toastr.success('Successfully registered. Kindly log in now', 'Success');
        
        setTimeout(() => window.location = '/Account/LogIn', 3000 );
    }
}