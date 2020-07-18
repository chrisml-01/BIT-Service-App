function validateLogon(){

    var username = document.forms["loginForm"]["uname"].value;
    var password = document.forms["loginForm"]["psw"].value;

    //LOGIN FORM
    if(username == null){
        alert("Username can not be left empty");
        return false;
    }
    if(password == null){
        alert("Password can not be left empty");
        return false;
    }

}