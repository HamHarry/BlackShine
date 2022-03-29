using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f; //กำหนดตัวแปรความเร็วการเครื่อนที่
    public float collisionOffset = 0.05f; //ตัวแปรการชดเชยการชน
    public ContactFilter2D movementFiller;  //ตัวกรองการเคลื่อนไหว


    Vector2 movementInput;  //ตัวแปรแบบ (x,y)
    SpriteRenderer spriteRenderer; //ตัวแปรSpriteRenderer
    Rigidbody2D rb; //ตัวแปรRigidbody2D
    Animator animator;  //ตัวแปรanimator
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();  //ตัวแปรการชน แบบเริ่มใหม่ทุกครั้ง

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  //เข้าถึงRigidbody2D (เรียกใช้) และเก็บไว้ใน rb 
        animator = GetComponent<Animator>(); //เข้าถึงAnimator (เรียกใช้) และเก็บไว้ใน animator
        spriteRenderer = GetComponent<SpriteRenderer>(); //เข้าถึงSpriteRenderer (เรียกใช้) และเก็บไว้ใน spriteRenderer
    }



    private void FixedUpdate() //ฟังก์ชั่น อัพเดตแบบคงที่(จำนวนครั้งต่อวินาทีทีแน่นอน)
    {

        /*<------------------------------------------------------MoveMent--------------------------------------------------------->*/

        if (movementInput != Vector2.zero) //ทำงานเมื่อ XY ไม่เท่ากับ 0
        {
            bool success = TryMove(movementInput); //ดึงฟังก์ชั่นมาใช้

            /*<--- sliding movement --->*/
            //เมื่อชนกับ raycast จะทำให้เราเลื่อนตามวีตถุที่ที่ชน จนพ้น
            if (!success && movementInput.x > 0)
            {
                success = TryMove(new Vector2(movementInput.x, 0)); //เครื่อนที่เฉพาะแกน X
            }
            //หาก X ล้มเหลว
            if (!success && movementInput.y > 0)
            {
                success = TryMove(new Vector2(0, movementInput.y));//เครื่อนที่เฉพาะแกน Y
            }

            /*<--- animator --->*/
            animator.SetBool("isMoving", success); //สั่งทำงาน isMoving ในBool
        }
        else
        {
            animator.SetBool("isMoving", false); //สั่งหลุดทำงาน isMoving ในBool
        }


            /*<--- flip character movement --->*/
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;  //ทำให้ตัวละครกลับด้านแกน x
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }

            /*<------------------------------------------------------------------------------------------------------------------------>*/



        }

        private bool TryMove(Vector2 direction) //ฟังก์ชั่น movement
        {
            /*<------------------------------------------------------MoveMent--------------------------------------------------------->*/
            //เมื่อมีข้อมูลการเคลื่อนไหวจาก'OnMove'แล้ว ก็นำไปคูณกับ "ความเร็ว"และ"เวลา" เพื่อหาว่าตัวละครเราเครื่อนที่เร็วแค่ไหนและไปทิศไหน
            if (direction != Vector2.zero)
            {
                //ตรวจสอบการชน ของraycast
                int count = rb.Cast(
                direction,   //ทิศทาง XY
                movementFiller,  //ตัวกรองการเคลื่อนไหว ซึ่งจะกดหนดสิ่งที่ raycast นี้ชนได้
                castCollisions,  //เก็บผลลัพธ์ของ raycast 
                moveSpeed * Time.fixedDeltaTime + collisionOffset); //ถ้า raycast ชนกัน จะได้Outputออกมาเป็น 1

                //ตรงนี้จึงสร้างเงื่อนไขมาลองรับว่าหากไม่พบการชนก็จะให้เคลื่อนที่ต่อไปได้
                if (count == 0)
                {
                    rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime); //ตำแหน่งของRigidbody + (ทิศทาง * ความเร็ว * เวลา) = ระยะทาง
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                // Can't move if there's no direction to move in
                return false;
            }

            /*<------------------------------------------------------------------------------------------------------------------------>*/
        }



        void OnMove(InputValue movementValue)  //รับค่า XY จากแป้นพิมพ์ และเรียกใช้ movementValue
        {
            movementInput = movementValue.Get<Vector2>(); //เอาค่า XY ที่ได้จากแป้นพิมพ์ มาเก็บในตัวแปร movementInput
        }


 }
