<!DOCTYPE html>
<html>
    <?php session_start(); ?>
<head>
	<!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <title>BIT Login</title>

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" type="text/css" href="assets/css/bootstrap.min.css" >
    <!-- Icon -->
    <link rel="stylesheet" type="text/css" href="assets/fonts/line-icons.css">
    <!-- Slicknav -->
    <link rel="stylesheet" type="text/css" href="assets/css/slicknav.css">
    <!-- Owl carousel -->
    <link rel="stylesheet" type="text/css" href="assets/css/owl.carousel.min.css">
    <link rel="stylesheet" type="text/css" href="assets/css/owl.theme.css">
    <!-- Slick Slider -->
    <link rel="stylesheet" type="text/css" href="assets/css/slick.css" >
    <link rel="stylesheet" type="text/css" href="assets/css/slick-theme.css" >
    <!-- Animate -->
    <link rel="stylesheet" type="text/css" href="assets/css/animate.css">
    <!-- Main Style -->
    <link rel="stylesheet" type="text/css" href="assets/css/main.css">
    <!-- Responsive Style -->
    <link rel="stylesheet" type="text/css" href="assets/css/responsive.css">
    <!-- LINE ICON -->
    <link href="https://cdn.lineicons.com/1.0.1/LineIcons.min.css" rel="stylesheet">
    <script src="assets/js/verifyLogon.js"></script>
</head>
<body>
<!-- Header Area wrapper Starts -->
    <header id="header-wrap">
      <!-- Navbar Start -->
      <nav class="navbar navbar-expand-lg fixed-top scrolling-navbar indigo">
        <div class="container">
          <!-- Brand and toggle get grouped for better mobile display -->
          <div class="navbar-header">
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#main-navbar" aria-controls="main-navbar" aria-expanded="false" aria-label="Toggle navigation">
              <span class="navbar-toggler-icon"></span>
              <span class="icon-menu"></span>
              <span class="icon-menu"></span>
              <span class="icon-menu"></span>
            </button>
            <!-- logo -->
            <a href="home.html" class="navbar-brand"><img id="logo"src="assets/img/BITlogo.png" alt=""></a>
          </div>
          <div class="collapse navbar-collapse" id="main-navbar">
            <ul class="navbar-nav mr-auto w-100 justify-content-left clearfix">
              <li>
                <a class="nav-link" href="home.html">
                  Home
                </a>
              </li>           
          </div>
        </div>

        <!-- Mobile Menu Start -->
        <ul class="mobile-menu navbar-nav">
          <li>
            <a class="page-scroll" href="home.html">
              Home
            </a>
          </li>      
        <!-- Mobile Menu End -->
    </nav>
    
     <!-- Hero Area Start -->
      <div id="hero-area" class="hero-area-bg">
        <div class="overlay"></div>
        <div class="container">
          <div class="row">
            <div class="col-md-12 col-sm-12">
              <!-- form -->
              <form name="loginForm" id="login-form" action="checkLogin.php" method="POST" onsubmit="return validateLogon()">
               
                <div class="login-container">
                   <h4 style="text-align: center;">Login</h4>
                  <label class = "label-login" for="uname"><b>Username</b></label>
                <input type="text" placeholder="Enter Username" name="uname" >

                <label class = "label-login" for="psw"><b>Password</b></label>
                <input type="password" placeholder="Enter Password" name="psw" >

                <input class="button" type="submit" name="submit" value="Login">
                <label class = "label-login">
                  <input type="checkbox" checked="checked" name="remember"> Remember me
                </label>
                <span class="psw"><b>Forgot </b><a id= "pswd"href="#">password?</a></span>
                </div>       
              </form>
                            
            </div>
          </div>
        </div>
      </div>
      <!-- Hero Area End -->
      <!-- CHECK LOGON PHP, to make the java script work -->
      <!-- checklogon.php here -->
      
  </header>

    <section id="copyright">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <p>Copyright © 2019 UIdeck All Right Reserved</p>
          </div>
        </div>
      </div>
    </section>   

    <!-- Go to Top Link -->
    <a href="#" class="back-to-top">
    	<i class="lni-arrow-up"></i>
    </a>
    
    <!-- Preloader -->
    <div id="preloader">
      <div class="loader" id="loader-1"></div>
    </div>
    <!-- End Preloader -->
    
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="assets/js/jquery-min.js"></script>
    <script src="assets/js/popper.min.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/owl.carousel.min.js"></script>
    <script src="assets/js/slick.min.js"></script>
    <script src="assets/js/wow.js"></script>
    <script src="assets/js/jquery.nav.js"></script>
    <script src="assets/js/scrolling-nav.js"></script>
    <script src="assets/js/jquery.easing.min.js"></script>
    <script src="assets/js/jquery.slicknav.js"></script>
    <script src="assets/js/main.js"></script>
    <script src="javascripts/validateForm.js"></script>
    
</body>
</html>