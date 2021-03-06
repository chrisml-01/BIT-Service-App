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
          <h1 class="mt-4">Rate our Service</h1> 
            <p>Tell us how you feel, I hope you were satisfied with our service!</p>

            <form name= "bookingForm" method="POST" action="newbooking.php">
              <label>Service:</label>
              <input class="formBig" type="text" name="bookingDate" disabled='true' value = '<?php if(isset($_POST['service'])){ echo $_POST['service'];}?>'>
              <label>Date:</label>
              <input class="formBig" type="date" name="bookingDate" value = '<?php if(isset($_POST['bookdate'])){ echo $_POST['bookdate'];}?>' required>
              <br>
              <label>Contractors:</label>
              <br>
              <input class="formMore" name="requestDets" type="text" disabled='true' value = '<?php if(isset($_POST['contractorId']) && isset($_POST['fname']) && isset($_POST['sname'])){ echo $_POST['contractorId']. " ". $_POST['fname']. " ".$_POST['sname']  ;}?>' required></textarea>
              <p><input class="button" type="submit" name="submit" value="Rate Job"></p>
            </form>
        </div>
    </div>
    <!-- /#page-content-wrapper -->

    <?php  
    if(isset($_POST['service']) && isset($_POST['bookingDate']) && isset($_POST['time']) && isset($_POST['street']) && isset($_POST['suburb']) && isset($_POST['state']) && isset($_POST['postcode']) && isset($_POST['requestDets']) && isset($_POST['submit'])){
      
      $db = mysqli_connect('localhost', 'root', '','bit_luntok');
      $clientid = $_SESSION["Clientid"];
      $currentDate = date('Y-m-d');
      $status = "Awaiting for Approval";

      $bookingDate = $_POST["bookingDate"];
      
      $queryStr2 = "insert INTO booking (ClientId, SkillId, Date, BookingDate, preferredTime , Street, Suburb, State, PostCode, Status, Notes) 
            VALUES ('".$clientid."',
            '".$_POST["service"]."',
            '".$currentDate."',
            '".$bookingDate."',
            '".$_POST["time"]."',
            '".$_POST["street"]."',
            '".$_POST["suburb"]."',
            '".$_POST["state"]."',
            '".$_POST["postcode"]."', 
            '".$status."',            
            '".$_POST["requestDets"]."')";

      $result2 = mysqli_query($db, $queryStr2);  

      if($result2){
        ?>
          <script>
              alert('Request sent!');
              window.location = 'newbooking.php';
          </script>
        <?php
      }else{
        ?>
          <script>
              alert('Sending failed!');
              window.location = 'newbooking.php';
          </script>
        <?php
      }    
      // echo ($clientid . "," . $_POST["service"] . "," . $currentDate . "," . $bookingDate . "," . $_POST["time"] . "," . $_POST["street"] . "," . $_POST["suburb"] . "," . $_POST["state"] . "," . $_POST["postcode"] . "," . $status . "," . $_POST["requestDets"]);
      // echo (date("w", strtotime($bookingDate)));
    } 
    ?> 

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
