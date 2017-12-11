using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingPage : MonoBehaviour {
    public InputField m_initial_position_text;
    public InputField m_initial_velocity_text;
    public InputField m_acceleration_text;
    public InputField m_time_text;
    public InputField m_location_text;
    public InputField m_velocity_text;
    public Text m_result_text;

    private double m_initial_position;
    private double m_initial_velocity;
    private double m_acceleration;
    private double m_time;
    private double m_location;
    private double m_velocity;

    private void Start()
    {
        m_acceleration_text.text = "-9.80665";
    }

    public void OnTimeClicked()
    {
        FetchInput();

        double velocity = 0.0;
        double position = 0.0;

        Calc(m_time, ref velocity, ref position);
        DisplayResult(MakeResult(m_time, velocity, position));
    }

    public void OnPositionClicked()
    {
        FetchInput();

        double a = 0.5 * m_acceleration;
        double b = m_initial_velocity;
        double c = m_initial_position - m_location;

        try
        {
            double t1 = (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
            double t2 = (-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
            // Debug.Log(t1 + "," + t2);

            if (!Double.IsInfinity(t1) && !Double.IsInfinity(t2))
            {
                if (t1 >= 0.0 && t2 >= 0.0 && !Double.IsInfinity(t1) && !Double.IsInfinity(t2))
                {
                    double velocity1 = 0.0;
                    double position1 = 0.0;
                    Calc(t1, ref velocity1, ref position1);
                    string result = MakeResult(t1, velocity1, position1);

                    result += "\n\n";

                    double velocity2 = 0.0;
                    double position2 = 0.0;
                    Calc(t2, ref velocity2, ref position2);

                    result += MakeResult(t2, velocity2, position2);

                    DisplayResult(result);
                }
                else if (t1 >= 0.0 || t2 >= 0.0)
                {
                    double t = t1 > 0.0 ? t1 : t2;

                    double velocity = 0.0;
                    double position = 0.0;

                    Calc(t, ref velocity, ref position);
                    DisplayResult(MakeResult(t, velocity, position));
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

    public void OnVelocityClicked()
    {
        FetchInput();

        double t = 0.0;

        try
        {
            t = (m_velocity - m_initial_velocity) / m_acceleration;

            if (t >= 0.0 && !Double.IsInfinity(t))
            {
                double velocity = 0.0;
                double position = 0.0;

                Calc(t, ref velocity, ref position);
                DisplayResult(MakeResult(t, velocity, position));
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

    private void FetchInput()
    {
        if(!double.TryParse(m_initial_position_text.text, out m_initial_position))
        {
            m_initial_position = 0.0;
            Debug.Log("F:m_initial_position");
        }
        else
        {
            Debug.Log("S:" + m_initial_position);
        }

        if (!double.TryParse(m_initial_velocity_text.text, out m_initial_velocity))
        {
            m_initial_velocity = 0.0;
            Debug.Log("F:m_initial_velocity");
        }
        else
        {
            Debug.Log("S:" + m_initial_velocity);
        }

        if (!double.TryParse(m_acceleration_text.text, out m_acceleration))
        {
            m_acceleration = 0.0;
            Debug.Log("F:m_acceleration");
        }
        else
        {
            Debug.Log("S:" + m_acceleration);
        }

        if (!double.TryParse(m_time_text.text, out m_time))
        {
            m_time = 0.0;
            Debug.Log("F:m_time");
        }
        else
        {
            Debug.Log("S:" + m_time);
        }

        if (!double.TryParse(m_location_text.text, out m_location))
        {
            m_location = 0.0;
            Debug.Log("F:m_location");
        }
        else
        {
            Debug.Log("S:" + m_location);
        }

        if (!double.TryParse(m_velocity_text.text, out m_velocity))
        {
            m_velocity = 0.0;
            Debug.Log("F:m_velocity");
        }
        else
        {
            Debug.Log("S:" + m_velocity);
        }
    }

    private void Calc(double time, ref double velocity, ref double position)
    {
        velocity = m_initial_velocity + m_acceleration * time;
        position = m_initial_position + m_initial_velocity * time + 0.5 * m_acceleration * time * time;
    }

    private string MakeResult(double time, double velocity, double position)
    {
        return string.Format("{0:F05} 초에\n" +
                      "위치 {1:F05} m,\n" +
                      "속도 {2:F05} m / s", time, position, velocity);
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
