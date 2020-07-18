<!DOCTYPE html>
<html lang="en">
<?php session_start(); ?>
<head>

  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">

  <title>BIT</title>

  <!-- Bootstrap core CSS -->
  <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">

  <!-- Custom styles for this template -->
  <link href="css/simple-sidebar.css" rel="stylesheet">

</head>

<body>

  <div class="d-flex" id="wrapper">

     <!-- Sidebar -->
     <?php 
      include ("template/sidebar.php");
    ?>
    <!-- Page Content -->
    <div id="page-content-wrapper">

    <!-- Nav -->
      <?php 
        include("template/client_nav.php");
      ?>

    <!-- Main Container -->
      <div class="container-fluid">
        
        <div class="container">
          <h1 class="mt-4">Job Details</h1> 
            <!-- <p>Tell us how you feel, I hope you were satisfied with our service!</p> -->

            <form name= "bookingForm" method="POST" action="coming-soon.html">
              <label>Service:</label>
              <input class="formBig" type="text" name="bookingDate" disabled='true' value = '<?php if(isset($_POST['service'])){ echo $_POST['service'];}?>'>
              <label>Date:</label>
              <input class="formBig" type="date" name="bookingDate" disabled='true' value = '<?php if(isset($_POST['bookdate'])){ echo $_POST['bookdate'];}?>' required>
              <br>
              <label>Contractors:</label>
              <br>
              <input class="formBig" name="requestDets" type="text" disabled='true' value = '<?php if(isset($_POST['contractorName'])){ echo $_POST['contractorName'];}?>' required></textarea>
              <p><input class="button" type="submit" name="submit" value="Rate Job"></p>
            </form>
        </div>
    </div>
    <!-- /#page-content-wrapper -->

  </div>
  <!-- /#wrapper -->

  <!-- Bootstrap core JavaScript -->
  <script src="vendor/jquery/jquery.min.js"></script>
  <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

  <!-- Menu Toggle Script -->
  <script>
    $("#menu-toggle").click(function(e) {
      e.preventDefault();
      $("#wrapper").toggleClass("toggled");
    });
  </script>

</body>

</html>
