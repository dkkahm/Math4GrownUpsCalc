using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectilePage : MonoBehaviour
{
    public InputField m_initial_velocity_text;
    public InputField m_initial_degree_text;
    public InputField m_acceleration_text;
    public InputField m_time_text;
    public InputField m_h_position_text;
    public InputField m_v_position_text;
    public InputField m_v_velocity_text;
    public Text m_result_text;

    private double m_initial_v_velocity;
    private double m_initial_h_velocity;
    private double m_initial_radian;
    private double m_acceleration;
    private double m_time;
    private double m_h_position;
    private double m_v_position;
    private double m_v_velocity;

    private void Start()
    {
        m_acceleration_text.text = "-9.80665";
    }

    public void OnTimeClicked()
    {
        FetchInput();

        if (m_time >= 0.0)
        {
            double h_velocity = 0.0;
            double h_position = 0.0;
            double v_velocity = 0.0;
            double v_position = 0.0;
            Calc(m_time, ref h_velocity, ref h_position, ref v_velocity, ref v_position);

            DisplayResult(MakeResult(m_time, h_velocity, v_velocity, h_position, v_position));
        }
        else
        {
            InvalidResult();
        }
    }

    public void OnHorizontalPositionClicked()
    {
        FetchInput();

        try
        {
            double t = m_h_position / m_initial_h_velocity;

            if (t >= 0.0 && !Double.IsInfinity(t))
            {
                double h_velocity = 0.0;
                double h_position = 0.0;
                double v_velocity = 0.0;
                double v_position = 0.0;
                Calc(t, ref h_velocity, ref h_position, ref v_velocity, ref v_position);

                DisplayResult(MakeResult(t, h_velocity, v_velocity, h_position, v_position));
            }
            else
            {
                InvalidResult();
            }
        }
        catch
        {
            InvalidResult();
        }
    }

    public void OnVertcalPositionClicked()
    {
        FetchInput();

        double a = 0.5 * m_acceleration;
        double b = m_initial_v_velocity;
        double c = -m_v_position;

        try
        {
            double t1 = (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
            double t2 = (-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
            // Debug.Log(t1 + "," + t2);

            if (!Double.IsInfinity(t1) && !Double.IsInfinity(t2))
            {
                if (t1 >= 0.0 && t2 >= 0.0)
                {
                    double h_velocity = 0.0;
                    double h_position = 0.0;
                    double v_velocity = 0.0;
                    double v_position = 0.0;
                    Calc(t1, ref h_velocity, ref h_position, ref v_velocity, ref v_position);
                    string result = MakeResult(t1, h_velocity, v_velocity, h_position, v_position);

                    result += "\n\n";

                    Calc(t2, ref h_velocity, ref h_position, ref v_velocity, ref v_position);
                    result += MakeResult(t2, h_velocity, v_velocity, h_position, v_position);

                    DisplayResult(result);
                }
                else if (t1 >= 0.0 || t2 >= 0.0)
                {
                    double t = t1 > 0.0 ? t1 : t2;

                    double h_velocity = 0.0;
                    double h_position = 0.0;
                    double v_velocity = 0.0;
                    double v_position = 0.0;
                    Calc(t, ref h_velocity, ref h_position, ref v_velocity, ref v_position);
                    DisplayResult(MakeResult(t, h_velocity, v_velocity, h_position, v_position));
                }
                else
                {
                    InvalidResult();
                }
            }
            else
            {
                InvalidResult();
            }
        }
        catch
        {
            InvalidResult();
        }
    }

    public void OnVerticalVelocityClicked()
    {
        FetchInput();

        double t = 0.0;

        t = (m_v_velocity - m_initial_v_velocity) / m_acceleration;

        if (t >= 0.0 && !Double.IsInfinity(t))
        {
            double h_velocity = 0.0;
            double h_position = 0.0;
            double v_velocity = 0.0;
            double v_position = 0.0;
            Calc(t, ref h_velocity, ref h_position, ref v_velocity, ref v_position);
            DisplayResult(MakeResult(t, h_velocity, v_velocity, h_position, v_position));
        }
        else
        {
            InvalidResult();
        }
    }

    private void FetchInput()
    {
        double initial_velocity = 0.0;
        if (!double.TryParse(m_initial_velocity_text.text, out initial_velocity))
        {
            initial_velocity = 0.0;
            // Debug.Log("F:initial_velocity");
        }
        else
        {
            // Debug.Log("S:initial_velocity" + initial_velocity);
        }

        double initial_degree = 0.0;
        if (!double.TryParse(m_initial_degree_text.text, out initial_degree))
        {
            initial_degree = 0.0;
            // Debug.Log("F:initial_degree");
        }
        else
        {
            // Debug.Log("S:initial_degree" + initial_degree);
        }

        if (!double.TryParse(m_acceleration_text.text, out m_acceleration))
        {
            m_acceleration = 0.0;
            // Debug.Log("F:m_acceleration");
        }
        else
        {
            // Debug.Log("S:m_acceleration" + m_acceleration);
        }

        if (!double.TryParse(m_time_text.text, out m_time))
        {
            m_time = 0.0;
            // Debug.Log("F:m_time");
        }
        else
        {
            // Debug.Log("S:" + m_time);
        }

        if (!double.TryParse(m_h_position_text.text, out m_h_position))
        {
            m_h_position = 0.0;
            // Debug.Log("F:m_h_position");
        }
        else
        {
            // Debug.Log("S:m_h_position" + m_h_position);
        }

        if (!double.TryParse(m_v_position_text.text, out m_v_position))
        {
            m_v_position = 0.0;
            // Debug.Log("F:m_v_position");
        }
        else
        {
            // Debug.Log("S:m_v_position" + m_v_position);
        }

        if (!double.TryParse(m_v_velocity_text.text, out m_v_velocity))
        {
            m_v_velocity = 0.0;
            // Debug.Log("F:m_v_velocity");
        }
        else
        {
            // Debug.Log("S:m_v_velocity" + m_v_velocity);
        }

        m_initial_radian = Math.PI * initial_degree / 180.0;

        m_initial_v_velocity = initial_velocity * Math.Sin(m_initial_radian);
        m_initial_h_velocity = initial_velocity * Math.Cos(m_initial_radian);

        Debug.Log(initial_degree);
        Debug.Log(m_initial_h_velocity);
        if (initial_degree == 90.0)
        {
            m_initial_h_velocity = 0.0;
            Debug.Log(m_initial_h_velocity);
        }
    }

    private void Calc(double time, ref double h_velocity, ref double h_position, ref double v_velocity, ref double v_position)
    {
        h_velocity = m_initial_h_velocity;
        h_position = m_initial_h_velocity * time;

        v_velocity = m_initial_v_velocity + m_acceleration * time;
        v_position = m_initial_v_velocity * time + 0.5 * m_acceleration * time * time;
    }

    private string MakeResult(double time, double h_velocity, double v_veloicy, double h_position, double v_position)
    {
        double speed = Math.Sqrt(h_velocity * h_velocity + v_veloicy * v_veloicy);

        return string.Format("발사각도 {0:F05} 라디안,\n" +
                      "발사속도 ({1:F05}, {2:F05})\n" +
                      "{3:F05} 초에 \n" +
                      "위치 ({4:F05}, {5:F05}),\n" +
                      "속도 ({6:F05}, {7:F05})\n" +
                      "속력 {8:F05} m / s",
                      m_initial_radian,
                      m_initial_h_velocity, m_initial_v_velocity,
                      time,
                      h_position, v_position,
                      h_velocity, v_veloicy,
                      speed);
    }

    private void InvalidResult()
    {
        DisplayResult("정상적인 결과를 얻을 수 없습니다.");
    }

    private void DisplayResult(string text)
    {
        m_result_text.text = text;
    }
}
