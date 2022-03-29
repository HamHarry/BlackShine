using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f; //��˹�����ä������ǡ������͹���
    public float collisionOffset = 0.05f; //����á�ê��¡�ê�
    public ContactFilter2D movementFiller;  //��ǡ�ͧ�������͹���


    Vector2 movementInput;  //�����Ẻ (x,y)
    SpriteRenderer spriteRenderer; //�����SpriteRenderer
    Rigidbody2D rb; //�����Rigidbody2D
    Animator animator;  //�����animator
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();  //����á�ê� Ẻ���������ء����

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  //��Ҷ֧Rigidbody2D (���¡��) ��������� rb 
        animator = GetComponent<Animator>(); //��Ҷ֧Animator (���¡��) ��������� animator
        spriteRenderer = GetComponent<SpriteRenderer>(); //��Ҷ֧SpriteRenderer (���¡��) ��������� spriteRenderer
    }



    private void FixedUpdate() //�ѧ���� �ѾവẺ�����(�ӹǹ���駵���Թҷշ���͹)
    {

        /*<------------------------------------------------------MoveMent--------------------------------------------------------->*/

        if (movementInput != Vector2.zero) //�ӧҹ����� XY �����ҡѺ 0
        {
            bool success = TryMove(movementInput); //�֧�ѧ��������

            /*<--- sliding movement --->*/
            //����ͪ��Ѻ raycast �з�����������͹����յ�ط���誹 ����
            if (!success && movementInput.x > 0)
            {
                success = TryMove(new Vector2(movementInput.x, 0)); //����͹���੾��᡹ X
            }
            //�ҡ X �������
            if (!success && movementInput.y > 0)
            {
                success = TryMove(new Vector2(0, movementInput.y));//����͹���੾��᡹ Y
            }

            /*<--- animator --->*/
            animator.SetBool("isMoving", success); //��觷ӧҹ isMoving �Bool
        }
        else
        {
            animator.SetBool("isMoving", false); //�����ش�ӧҹ isMoving �Bool
        }


            /*<--- flip character movement --->*/
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;  //��������Фá�Ѻ��ҹ᡹ x
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }

            /*<------------------------------------------------------------------------------------------------------------------------>*/



        }

        private bool TryMove(Vector2 direction) //�ѧ���� movement
        {
            /*<------------------------------------------------------MoveMent--------------------------------------------------------->*/
            //������բ����š������͹��Ǩҡ'OnMove'���� ���令ٳ�Ѻ "��������"���"����" ��������ҵ���Ф��������͹����������˹���价���˹
            if (direction != Vector2.zero)
            {
                //��Ǩ�ͺ��ê� �ͧraycast
                int count = rb.Cast(
                direction,   //��ȷҧ XY
                movementFiller,  //��ǡ�ͧ�������͹��� ��觨С�˹���觷�� raycast ��骹��
                castCollisions,  //�纼��Ѿ��ͧ raycast 
                moveSpeed * Time.fixedDeltaTime + collisionOffset); //��� raycast ���ѹ ����Output�͡���� 1

                //�ç���֧���ҧ���͹����ͧ�Ѻ����ҡ��辺��ê�����������͹��������
                if (count == 0)
                {
                    rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime); //���˹觢ͧRigidbody + (��ȷҧ * �������� * ����) = ���зҧ
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



        void OnMove(InputValue movementValue)  //�Ѻ��� XY �ҡ�鹾���� ������¡�� movementValue
        {
            movementInput = movementValue.Get<Vector2>(); //��Ҥ�� XY �����ҡ�鹾���� ����㹵���� movementInput
        }


 }
