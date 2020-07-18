<!DOCTYPE html>
<html lang="en">
<?php session_start(); 
if($_SESSION["Type"] != "Coordinator"){
  session_destroy();
  header("location:login.php");
}
?>
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
      <?php 
      //getting all 'Awaiting for Approval' job request
      $db = mysqli_connect('localhost', 'root', '', 'bit_luntok'); 
      $id = $_SESSION["Coordinatorid"];
      $queryStr = "select bookingId, clientId, skillId, skillname, bookingdate, preferredTime, suburb 
      FROM view_openjobrequest";
      
      $result = mysqli_query($db, $queryStr);
      
      if(mysqli_num_rows($result)):
      ?>
        <h3 class="mt-4">All Open Job Request</h3>

        <?php 
        echo "<table class = 'table'>";
        echo "<tr><th id = 'top-left'>Booking ID</th><th>Client ID</th><th>Service</th><th>Date</th><th>Suburb</th><th class = 'top-right'>Assign</th></tr>";
        while($row = mysqli_fetch_array($result)){
          echo "<form name='jobOpen' method='POST' action='assignJob.php'>";
          echo "<tr><td>".$row["bookingId"]."</td><td>".$row["clientId"]."</td><td>".$row["skillname"]."</td><td>".$row["bookingdate"]."</td><td>".$row["suburb"]."<td>
          <a href='assignJob.php'><input name = 'assign' class = 'tableButton' type='submit' value ='Assign'></a></td>
          <input name='bookingId' type='hidden' value='".$row["bookingId"]."'>
          <input name='clientId' type='hidden' value='".$row["clientId"]."'>
          <input name='skill' type='hidden' value='".$row["skillId"]."'>
          <input name='bookDate' type='hidden' value='".$row["bookingdate"]."'>
          <input name='preferredTime' type='hidden' value='".$row["preferredTime"]."'>
          <input name='suburb' type='hidden' value='".$row["suburb"]."'></tr>";  
          echo "</form>";
        }
        echo "</table>";

        else:
        ?>  
        <h3 class="mt-4">There are no open requests at the moment</h3>

      <?php endif;?>
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
