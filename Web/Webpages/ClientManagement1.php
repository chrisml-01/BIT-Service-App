<!DOCTYPE html>
<html lang="en">
<?php session_start();
    if($_SESSION["Type"] != "Client"){
      session_destroy();
      header("location:login.php");
    }

    if($_SESSION["Clientid"] == null){
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
      <?php 
      //getting all the bookings (open) of the logined person
      $db = mysqli_connect('localhost', 'root', '', 'bit_luntok'); 
      $id = $_SESSION["Clientid"];
      $queryStr = "select skillName, bookingDate, suburb, status 
      FROM view_clientopenrequest WHERE ClientId =" .$id;
      $result = mysqli_query($db, $queryStr);
      
      if(mysqli_num_rows($result)):
      ?>
        <h3 class="mt-4">All Open Request</h3>
      
      <?php   
        echo "<table class = 'table'>";
        echo "<tr><th id = 'top-left'>Service</th><th>Date</th><th>Suburb</th><th class = 'top-right'>Status</th></tr>";
        while($row = mysqli_fetch_array($result)){
          echo "<tr><td>".$row["skillName"]."</td><td>".$row["bookingDate"]."</td><td>".$row["suburb"]."</td><td>".$row["status"]."</td></tr>";  
        }
        echo "</table>";
      
      else:
        ?>
        <h3 class="mt-4">You have no open request at the moment</h3>

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
