<?php
    session_start();
    $db = mysqli_connect('localhost', 'root', '', 'bit_luntok'); 
    $id = $_SESSION["Coordinatorid"];

      
    $selectedButton = $_GET['btnApprove'];
    
    $bookingId = $_GET['bookingId'];  
    $contractorId =  $_GET['contractorId'];

    //echo $bookingId. ", ". $contractorId;

    $queryStr = "update service_booking SET service_booking.Status = 'Sent for Payment' 
    WHERE BookingId = " .$bookingId. " and ContractorId = " .$contractorId;
    
    $queryStr2 = "update service_booking SET service_booking.Status = 'Request Denied' 
    WHERE BookingId = " .$bookingId. " and ContractorId = " .$contractorId;


    switch ($selectedButton) {
        case 'Approve':
            $result = mysqli_query($db, $queryStr);
            if($result){
                ?>
                    <script>
                        alert('Job request approved!');
                        window.location = 'jobApproval.php';
                    </script>
                <?php     
            }
            break;
        case 'Disapprove':
            $result2 = mysqli_query($db, $queryStr2);
            if($result2){
                ?>
                    <script>
                        alert('Job request denied!');
                        window.location = 'jobApproval.php';
                    </script>
                <?php 
            }
            break;
        default:
            echo "Failed";
            break;
    }
     
?>