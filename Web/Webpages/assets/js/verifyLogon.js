function verifyLogon(){
    var form = document.getElementById("login-form");
    form.addEventListener("submit", function(event){
        event.preventDefault();
        alert("Username or password is incorrect!");
    });
    
}