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
          <h3 class="mt-4">All Available Contractors</h3>
          <?php 
          //getting all 'Awaiting for Approval' job request
      if(isset($_POST['assign'])){
          $db = mysqli_connect('localhost', 'root', '', 'bit_luntok'); 
          $id = $_SESSION["Coordinatorid"];

          $bookingId = $_POST['bookingId'];
          $clientId = $_POST['clientId'];
          $skill = $_POST['skill'];
          $date = date("w", strtotime($_POST['bookDate']));
          $time = $_POST['preferredTime'];
          $suburb = $_POST['suburb'];

          //echo $bookingId. $clientId. $date. $skill . $suburb. $time;
          $queryRejected = "select * FROM rejected_job";
          $resultRejected = mysqli_query($db, $queryRejected);
          
          

          $queryStr = "CALL usp_searchAvailableContractor('".$time."','".$date."','".$skill."','".$suburb."')";
         
          $result = mysqli_query($db, $queryStr);
          echo "<table class = 'table'>";
          echo "<tr><th id = 'top-left'>Contractor ID</th><th>First Name</th><th>Surname</th><th>Start Time</th><th>End Time</th><th class = 'top-right'>Send Job</th></tr>";
          while($row = mysqli_fetch_array($result)){
            echo "<form name='jobOpen' method='POST' action='assignJob.php'>";
            echo "<tr><td>".$row["contractorId"]."</td><td>".$row["firstName"]."</td><td>".$row["surName"]."</td><td>".$row["StartTime"]."</td><td>".$row["EndTime"]."</td>
            <td><input name = 'send' class = 'tableButton' type='submit' value ='Send'></td></tr>
            <input name='contractorId' type='hidden' value='".$row["contractorId"]."'>
            <input name='bookingId' type='hidden' value='".$bookingId."'>
            <input name='clientId' type='hidden' value='".$clientId."'>
            <input name='bookdate' type='hidden' value='".$_POST['bookDate']."'>
            <input name='booktime' type='hidden' value='".$time."'></tr>";  
            echo "</form>";
          }
          echo "</table>";         
        } 
        include_once("sendJob.php");
          ?>
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
