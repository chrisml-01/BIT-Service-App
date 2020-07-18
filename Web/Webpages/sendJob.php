<?php
if(isset($_POST['send'])) {
     $db = mysqli_connect('localhost', 'root', '', 'bit_luntok'); 
     $id = $_SESSION["Coordinatorid"];

     $contractorId = $_POST['contractorId'];
     $bookingId = $_POST['bookingId'];
     $clientId = $_POST['clientId'];
     $time = $_POST['booktime'];
     $bookdate = $_POST['bookdate'];

    //echo $contractorId.$bookingId.$clientId;
    //echo $time.$bookdate;

    $queryStr = "insert INTO service_booking(BookingId, ClientId, ContractorId, Status) 
     VALUES ('".$bookingId."','".$clientId."','".$contractorId."' ,'Pending')";

    // $queryStr2 = "insert INTO booking_contractors(contractorId, bookingId, clientId) 
    //  VALUES ('".$contractorId."','".$bookingId."','".$clientId."')";

    $queryStr3 = "update booking SET CoordinatorId = " .$id. " , Status = 'Pending' WHERE BookingId = " .$bookingId;
     
    // $queryStr4 = "Insert into roster (ContractorId, BookDate, BookTime) 
    // VALUES ('".$contractorId."', '".$bookdate."' , '".$time."')";

    $result = mysqli_query($db, $queryStr);
    // $result2 = mysqli_query($db, $queryStr2);
    $result3 = mysqli_query($db, $queryStr3);
    // $result4 = mysqli_query($db, $queryStr4);
     if($result && $result3){ 
       ?>
          <script>
              alert('Job sent!');
              window.location = 'coordinatorManagement.php';
          </script>
     <?php     
     }else{
      ?>
      <script>
              alert('Sending Failed!');
              window.location = 'coordinatorManagement.php';
      </script>
 <?php   
     }
    //  echo "</table>";
}
?>
