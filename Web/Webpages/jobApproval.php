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
        <?php 
        //getting all the bookings (open) of the logined person
        $db = mysqli_connect('localhost', 'root', '', 'bit_luntok'); 
        $id = $_SESSION["Coordinatorid"];
        $queryStr = "select bookingId, ContractorId , ContractorName , SkillName , bookingdate, TotalHours, TotalDistancedTravelled, ClientId, ClientName
        FROM view_approvaldets
        WHERE CoordinatorId =" .$id;
        
        $result = mysqli_query($db, $queryStr);
        
        if(mysqli_num_rows($result)):
        ?>
            <h3 class="mt-4">All Job Approval</h3>
        <?php 
        
        echo "<table class = 'table'>";
        echo "<tr><th id = 'top-left'>Booking ID</th><th>Contractor ID</th><th>Service</th><th>Booking Date</th><th class = 'top-right'></th></tr>";
        while($row = mysqli_fetch_array($result)){
          echo "<form name='jobHistory' method='POST' action='moreDets.php'>";
          echo "<tr><td>".$row["bookingId"]."</td><td>".$row["ContractorId"]."</td><td>".$row["SkillName"]."</td><td>".$row["bookingdate"]."</td>
          </td><td><input id = 'btnSeeMore' type='submit' name='btnMore' value ='View More Details' ></td>
          <input name='contractorId' type='hidden' value='".$row["ContractorId"]."'>
          <input name='contractorName' type='hidden' value='".$row["ContractorName"]."'></tr>";  
          echo "</form>"; 
        }
        echo "</table>";
        
      else:
        ?>
        <h3 class="mt-4">You have no job approvals at the moment</h3>

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
