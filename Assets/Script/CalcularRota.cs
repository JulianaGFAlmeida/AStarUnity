using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CalcularRota : MonoBehaviour
{
    List<Waypoint> waypoints;
    Waypoint novo;
    int contador=0;
    float speed=5f;
    bool mover=false;
    public Transform t;
    public Waypoint way;

    void Start()
    {
        waypoints = new List<Waypoint>();
        Rota(way, t.position);
        mover = true;
        
    }

    private void Update()
    {
        if (mover) {
            
            if (Vector3.Distance(this.transform.position, waypoints[contador].GetComponent<Transform>().position) > 0)
            {
                this.transform.position=  Vector3.MoveTowards(this.transform.position, waypoints[contador].GetComponent<Transform>().position, speed * Time.deltaTime);
            }
            else
            {
                if (contador < waypoints.Count-1)
                {
                    contador++;
                }
                else {
                    waypoints = new List<Waypoint>();
                    contador = 0;
                    mover = false;
                }
            }
        }
    }

    
    public void Rota(Waypoint wp, Vector3 pos)
    {
        if (waypoints.Count > 0)
        {
            Vector2[] wppos = new Vector2[wp.wps.Count]; print(wp.wps.Count);

            float[] custos = new float[wp.wps.Count];

            if (wppos.Length != 1)
            {
                for (int i = 0; i < wppos.Length; i++)
                {
                    wppos[i] = new Vector2(wp.wps[i].GetComponent<Transform>().position.x, wp.wps[i].GetComponent<Transform>().position.y);
                    custos[i] = Vector2.Distance(pos, wppos[i]);
                }
                
                BubbleSort(custos, custos.Length);

                for (int i = 0; i < wppos.Length; i++)
                {
                    if (Vector2.Distance(pos, wppos[i]) == custos[0])
                    {
                        novo = wp.wps[i];
                        break;
                    }
                }
                if (Vector2.Distance(pos, novo.GetComponent<Transform>().position) < 0.2)
                {
                    waypoints.Add(novo);

                }
                else
                {
                    for (int i = 0; i < novo.wps.Count; i++)
                    {
                        if (wp.transform.position == novo.wps[i].transform.position)
                        {
                            novo.wps.RemoveAt(i);
                            break;
                        }
                    }
                    waypoints.Add(novo);
                    Rota(novo, pos);

                }

            }
            else
            {
                novo = wp.wps[0];
                for (int i = 0; i < novo.wps.Count; i++)
                {
                    if (wp.transform.position == novo.wps[i].transform.position)
                    {
                        novo.wps.RemoveAt(i);
                        break;
                    }
                }
                if (Vector2.Distance(pos, novo.GetComponent<Transform>().position) < 0.2)
                {
                    waypoints.Add(novo);

                }
                else
                {
                    waypoints.Add(novo);
                    Rota(novo, pos);
                }

            }

        }
        else
        {

            waypoints.Add(wp);
            Rota(wp, pos);

        }
    }

     void BubbleSort(float[] arr, float length)
    {

        for (int i = 0; i < length - 1; i++)
        {
            for (int j = 0; j < length - (i + 1); j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    float repos;
                    repos = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = repos;
                }
            }
        }
    }



}
