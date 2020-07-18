<!DOCTYPE html>
<html lang="en">
<?php session_start(); 
  if($_SESSION["Type"] != "Contractor"){
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
   h3, h4{
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

    <!-- Nav -->
      <?php 
        include("template/contractor_nav.php");
      ?>

    <!-- Main Container -->
      <div class="container-fluid">

      <?php 
       $db = mysqli_connect('localhost', 'root', '', 'bit_luntok'); 
       $id = $_SESSION["Contractorid"];
       //getting all the bookings (open) of the logined person
       $queryStr = "select bookingId, ClientId ,skillName, bookingDate, preferredTime, Suburb 
       FROM view_contractoropenrequest WHERE ContractorId =" .$id; 

       $result = mysqli_query($db, $queryStr);

      if(mysqli_num_rows($result)):
      ?>
        <h3 class="mt-4">All Open Request</h3>

        <?php 
         echo "<table class = 'table'>";
         echo "<tr><th id = 'con-top-left'>Service</th><th style='width: 20%'>Date</th><th>Preferred Time</th><th>Suburb</th><th id= 'accept-th'>Accept</th><th id = 'top-right'>Reject</th></tr>";        
         while($row = mysqli_fetch_array($result)){
           echo "<form name='jobOpen' method='POST' action='acceptJob.php'>";
           echo "<tr><td>".$row["skillName"]."</td><td>".$row["bookingDate"]."</td><td>".date('g:i A', strtotime($row["preferredTime"]))."</td><td>".$row["Suburb"]."</td>
           <td><input name='btnAccept' class = 'tableButton' type='submit' value ='accept'></td>
           <td><input name='btnAccept' class = 'tableButton' type='submit' value ='reject'></td>
           <input name='bookingId' type='hidden' value='".$row["bookingId"]."'>
           <input name='booktime' type='hidden' value='".$row["preferredTime"]."'>
           <input name='bookdate' type='hidden' value='".$row["bookingDate"]."'>
           <input name='clientId' type='hidden' value='".$row["ClientId"]."'></tr>";  
           echo "</form>";
         }
         echo "</table>";

      else:
        ?>
        <h4 class="mt-4">You have no open job assignment request at the moment</h4>

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
