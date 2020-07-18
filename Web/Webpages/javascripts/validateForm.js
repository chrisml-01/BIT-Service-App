function validateForm(){
    var notes = document.forms["bookingForm"]["requestDets"].value;
    var postcode = document.forms["bookingForm"]["postcode"].value;
    var bookdate = new Date(document.forms["bookingForm"]["bookingDate"].value);
    var currentDate = new Date(Date.now());

    // var username = document.forms["loginForm"]["uname"].value;
    // var password = document.forms["loginForm"]["psw"].value;

    //NEW BOOKING FORM
    if(bookdate < currentDate){
        alert("You are not allowed to book a date from the past. Please try again.");
        return false;
    }
    if(notes.length > 180){
        alert("Please make sure that your input doesn't exceed up to 180 characters.");
        return false;
    }
    if (postcode.length > 4){
        alert("Postcode must be 4 characters.");
        return false;
    }
    


    // //LOGIN FORM
    // if(username == null){
    //     alert("Username can not be left empty");
    //     return false;
    // }
    // if(password == null){
    //     alert("Password can not be left empty");
    //     return false;
    // }

}