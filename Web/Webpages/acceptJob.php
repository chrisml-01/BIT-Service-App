<?php
    session_start();
    $db = mysqli_connect('localhost', 'root', '', 'bit_luntok'); 
    $id = $_SESSION["Contractorid"];
    
    $selectedButton = $_POST['btnAccept'];

    $bookingId = $_POST['bookingId'];
    $booktime = $_POST["booktime"];
    $bookdate = $_POST["bookdate"];
    $clientId = $_POST["clientId"];

    echo $id. " " .$bookingId.$clientId.$bookdate.$booktime;

    $queryStr = "update service_booking SET service_booking.Status = 'In Progress' 
    WHERE BookingId = " .$bookingId. " and ContractorId = ".$id;

    $queryStr2 = "update booking SET CoordinatorId = null , Status = 'Awaiting for Approval' WHERE BookingId = " .$bookingId;

    $queryStr4 = "insert INTO rejected_job(BookingId, ClientId, ContractorId, Status) 
    VALUES ('".$bookingId."','".$clientId."','".$id."','Rejected')";
    
    // $queryStr4 = "update service_booking SET service_booking.Status = 'Rejected' 
    // WHERE BookingId = " .$bookingId. " and ContractorId = ".$id;
    
    $queryStr4 = "delete FROM service_booking WHERE BookingId = " .$bookingId. " and ContractorId = ".$id;

    $queryStr5 = "update booking SET booking.Status = 'In Progress' WHERE BookingId = " .$bookingId;

    //$queryStr6 = "delete FROM service_booking WHERE BookingId = " .$bookingId. " and ContractorId = ".$id;
    

    switch ($selectedButton) {
        case 'accept':
            $result = mysqli_query($db, $queryStr);
            $result5 = mysqli_query($db, $queryStr5);
            // $result6 = mysqli_query($db, $queryStr6);
            if($result && $result5){
                ?>
                    <script>
                        alert('Job request accepted!');
                        window.location = 'ContractorManagement.php';
                    </script>
                <?php     
            }
            break;
        case 'reject':
            $result2 = mysqli_query($db, $queryStr2);
            $result4 = mysqli_query($db, $queryStr4);
            
            if($result2 && $result4){
                ?>
                    <script>
                        alert('Job request rejected!');
                        window.location = 'ContractorManagement.php';
                    </script>
                <?php 
            }
            break;
        default:
            echo "Failed";
            break;
    }
     
    
    

?>