<!DOCTYPE html>
<html lang="en">
<?php session_start(); ?>
<head>

 <?php 
    include("template/head.php")
 ?>

</head>
<style>
  h3, h4, h2, h1{
  text-align: center;
}
</style>
<body>
    
  <div class="d-flex" id="wrapper">

    <!-- Sidebar -->
   <!-- Sidebar -->
    <div class="backgroundSidebar" id="sidebar-wrapper">
      <div class="sidebar-heading"><a href="coordinatorManagement.php"><img id ="logoDash"src="assets/img/BITlogo.png"></a></div>
      <div class="list-group list-group-flush">
        <a href="coordinatorManagement.php" class="sidebarList">Dashboard</a>
        <a href="jobApproval.php" class="sidebarList">Job Approval</a>
        <a href="coordJobHistory.php" class="sidebarList">Job History</a>
      </div>
    </div>
    <!-- /#sidebar-wrapper -->

    <!-- Page Content -->
    <div id="page-content-wrapper">

    <!-- nav -->
      <?php
          include("template/coord_nav.php");
      ?>

    <!-- Main Container -->
      <div class="container-fluid">
        <!-- <h3 class="mt-4">Job Approval</h3> -->
        <?php 
        //getting all the bookings (open) of the logined person
        if(isset($_POST['btnMore'])){
        $db = mysqli_connect('localhost', 'root', '', 'bit_luntok'); 
        $id = $_SESSION["Coordinatorid"];

        $contractorId = $_POST['contractorId'];
        $contractorName = $_POST['contractorName'];

        // echo $id. ", ".$contractorId;

        $queryStr = "select bookingId, ContractorId ,SkillName , bookingdate, TotalHours, TotalDistancedTravelled, ClientId, ClientName
        FROM view_approvaldets
        WHERE CoordinatorId =" .$id. " and ContractorId = " .$contractorId;

        $result = mysqli_query($db, $queryStr);

        while($row = mysqli_fetch_array($result)){

          $bookingId = $row["bookingId"];
          $service = $row["SkillName"];
          $bookingdate = $row["bookingdate"];
          $totalhrs = $row["TotalHours"];
          $totalkm = $row["TotalDistancedTravelled"];
          $clientId = $row["ClientId"];
          $clientName = $row["ClientName"];

        }

        }
        ?>

      <?php if(isset($bookingId) && isset($contractorId)) { ?>
        <div class="container">
            <h1 class="mt-4">Job Details</h1> 
              <form name= "bookingForm" method="GET" action="approveJob.php">
                <label>Booking ID:</label> 
                <input class="formBig"  type="text" name="booking" disabled ='true' value = '<?= $bookingId ?>'>
                <input name='bookingId' type='hidden' value='<?= $bookingId ?>'>
                <label>Service:</label>
                <input class="formBig"  type="text" name="service" disabled ='true' value = '<?= $service ?>'>
                <label>Date:</label>
                <input class="formBig"  type="text" name="bookingdate" disabled ='true' value = '<?= $bookingdate ?>'>
                <label>Client:</label>
                <input class="formBig"  type="text" name="client" disabled ='true' value = '<?= $clientId. ", " .$clientName ?>'>
                <label>Contractor:</label>
                <input class="formBig"  type="text" name="contractor" disabled ='true' value = '<?= $contractorId. ", " .$contractorName ?>'>
                <input name='contractorId' type='hidden' value='<?= $contractorId ?>'>
                <label>Total Hours:</label>
                <input class="formBig"  type="text" name="hours" disabled ='true' value = '<?= $totalhrs?>'>
                <br>
                <label>Total Distanced Travelled (km):</label>
                <input class="formBig" type="text" name="distance" disabled ='true' value = '<?= $totalkm. " km" ?>'>
                <br>
                <input id="btnApprove" type="submit" name="btnApprove" value="Approve">
                <input id="btnDisapprove" type="submit" name="btnApprove" value="Disapprove">
              </form>
          </div>        
      <?php 
        };?>
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
