<?php 
    session_start();
    // unset( $_SESSION["Clientid"]);
    // unset( $_SESSION["Username"]);
    session_destroy();
    header("location:login.php");

?>