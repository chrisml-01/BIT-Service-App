<!DOCTYPE html>
<html lang="en">
<?php session_start(); ?>
<head>

 <?php 
    include("template/head.php")
 ?>

</head>
<style>
  h3{
  text-align: center;
}
</style>
<body>
    
  <div class="d-flex" id="wrapper">

    <!-- Sidebar -->
   <!-- Sidebar -->
    <div class="backgroundSidebar" id="sidebar-wrapper">
      <div class="sidebar-heading"><a href="ContractorManagement.php"><img id ="logoDash"src="assets/img/BITlogo.png"></a></div>
      <div class="list-group list-group-flush">
        <a href="ContractorManagement.php" class="sidebarList">Dashboard</a>
        <!-- <a href="#" class="sidebarList">Profile</a> -->
        <a href="jobHistory.php" class="sidebarList">Job History</a>
      </div>
    </div>
    <!-- /#sidebar-wrapper -->

    <!-- Page Content -->
    <div id="page-content-wrapper">

    <!-- nav -->
      <?php 
        include("template/contractor_nav.php");
      ?>

    <!-- Main Container -->

      <div class="container-fluid">
        <div class="container">
        <h1 class="mt-4">Submit for Completion</h1> 
            <p>To submit a request for payment approval for your <br> completed job, please fill up and submit the form.</p>
            <form name= "bookingForm" method="POST" action="clocking.php">
              <label>Status:</label>
              <select class="formBig" name="status" required>
                  <option id="option">In Progress</option>
                  <option id="option">Completed</option>
                  </select>
              <label>Service:</label>
              <input class="formBig"  type="text" name="service" disabled ='true' value = '<?php if(isset($_POST['service'])){ echo $_POST['service'];}?>' required>
              <label>Start Time: (AM/PM)</label>
              <input class="formBig"  type="time" name="startT" min="08:00:00" max="17:00:00" required>
              <label>End Time: (AM/PM)</label>
              <input class="formBig"  type="time" name="endT" min="08:00:00" max="17:00:00" required>
              <br>
              <label>Total Distanced Travelled (km):</label>
              <input class="formBig" type="number" step="0.01" name="distance" required>
              <br>
              <p><input class="button" type="submit" name="submit" value="Submit for Approval"></p>
              <input name='bookingId' type='hidden' value='<?php if(isset($_POST['bookingId'])){ echo $_POST['bookingId'];}?>'>
              <input name='clientId' type='hidden' value='<?php if(isset($_POST['clientId'])){ echo $_POST['clientId'];}?>'>
              <input name='service_status' type='hidden' value='<?php if(isset($_POST['service_status'])){ echo $_POST['service_status'];}?>'>
            </form>
        </div>
      </div>
    </div>
    <!-- /#page-content-wrapper -->

  </div>
  <!-- /#wrapper -->

  <?php 
  $service_status = $_POST["service_status"];
  
  if(isset($_POST['startT']) && isset($_POST['endT']) && isset($_POST['distance']) && isset($_POST['submit'])){
    $db = mysqli_connect('localhost', 'root', '','bit_luntok');
    $id = $_SESSION["Contractorid"];  

    $bookingId = $_POST["bookingId"];
    $startT = $_POST['startT'];
    $endT = $_POST['endT'];
    $clientId = $_POST["clientId"];
    $distance = $_POST["distance"];
    $status = $_POST["status"];
    

    // echo $bookingId.",".$clientId.",".$id.",".$distance.",".$status. ",".$service_status;

    if($service_status == "Request Denied"){
      $queryUpdatePay = "Update pay_booking SET TotalDistancedTravelled = '".$distance."'
      WHERE BookingId = " .$bookingId. " and ContractorId = " .$id;

      $queryStr2 = "Update service_booking SET StartTime = '" .$startT. "' , EndTime = '".$endT. "',   Status = '".$status."' 
      WHERE BookingId = " .$bookingId. " and ContractorId = " .$id;

    } 

    if($service_status == "In Progress"){

      $queryStr = "Update service_booking SET StartTime = '" .$startT. "' , EndTime = '".$endT. "',   Status = '".$status."' 
      WHERE BookingId = " .$bookingId. " and ContractorId = " .$id;
      
      $queryStr3 = "insert INTO pay_booking(BookingId, ClientId, ContractorId ,TotalDistancedTravelled) 
      VALUES ('".$bookingId."',
      '".$clientId."',
      '".$id."',
      '".$distance."')";
         
      $queryStr4 = "Update booking SET Status = '".$status."' WHERE BookingId = " .$bookingId;
    }

    $resultUd = mysqli_query($db, $queryUpdatePay);
    $result2 = mysqli_query($db, $queryStr2);  
    
    
    $result = mysqli_query($db, $queryStr); 
    $result3 = mysqli_query($db, $queryStr3);     
    $result4 = mysqli_query($db, $queryStr4);
    

    $queryCall = "CALL usp_CalculateTime(".$bookingId.", ".$id.")";
    $result5 = mysqli_query($db,$queryCall);

    if($resultUd && $result2 && $result5){
      ?>
          <script>
              alert('Request Sent!');
              window.location = 'submitJob.php';
          </script>
      <?php
    } else if($result && $result3 && $result4 && $result5){
      ?>
          <script>
              alert('Request Sent!');
              window.location = 'submitJob.php';
          </script>
      <?php
    }else{
      ?>
          <script>
              alert('Sent Failed!');
              window.location = 'submitJob.php';
          </script>
      <?php
    } 
  }
 
  ?>

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
