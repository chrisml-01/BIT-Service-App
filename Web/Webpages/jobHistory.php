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
        <h3 class="mt-4">Job History</h3>
        <?php 
        //getting all the job bookings of the logined person
        $db = mysqli_connect('localhost', 'root', '', 'bit_luntok'); 
        $id = $_SESSION["Contractorid"];
        $queryStr = "select DISTINCT bookingId, clientId ,skillname, bookingdate, Suburb, Status 
        FROM view_contractorhistory where ContractorId = " .$id;

        
        $result = mysqli_query($db, $queryStr);
        echo "<table class = 'table'>";
        echo "<tr><th id = 'top-left'>BookingID</th><th>Service</th><th>Date</th><th class = 'top-right'>Status</th></tr>";
        while($row = mysqli_fetch_array($result)){
          echo "<form name='jobHistory' method='POST' action='clocking.php'>";
          echo "<tr><td>".$row["bookingId"]."</td><td>".$row["skillname"]."</td><td>".$row["bookingdate"]."</td><td>".$row["Status"]."</td>
          <input name='service' type='hidden' value='".$row["skillname"]."'>
          <input name='bookingId' type='hidden' value='".$row["bookingId"]."'>
          <input name='clientId' type='hidden' value='".$row["clientId"]."'></tr>"; 
          echo "</form>"; 
        }
        echo "</table>";
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
