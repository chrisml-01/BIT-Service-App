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
        <h3 class="mt-4">Request History</h3>
        <?php 
        //getting all the bookings (open) of the logined person
        $db = mysqli_connect('localhost', 'root', '', 'bit_luntok'); 
        $id = $_SESSION["Clientid"];
        $queryStr = "select BookingId, ClientId, Date, SkillName, BookingDate, Status, ContractorName
        FROM view_clienthistory WHERE ClientId = " .$id;

        $result = mysqli_query($db, $queryStr);
        echo "<table class = 'table'>";
        echo "<tr><th  id = 'top-left'>Date Booked</th><th>Service</th><th>Date Start</th><th>Status</th><th class = 'top-right'></th></tr>";
        while($row = mysqli_fetch_array($result)){
          echo "<form name='reqHistory' method='POST' action='jobRequestMoreDets.php'>";
          echo "<tr><td>".$row["Date"]."</td><td>".$row["SkillName"]."</td>
          <td>".$row["BookingDate"]."</td><td>".$row["Status"]."</td>
          <td><input id = 'btnSeeMore' type='submit' value ='View More Details' ></td>
          <input name='service' type='hidden' value='".$row["SkillName"]."'>
          <input name='bookdate' type='hidden' value='".$row["BookingDate"]."'>
          <input name='contractorName' type='hidden' value='".$row["ContractorName"]."'></tr>";  
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
