using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollPages : MonoBehaviour {
    
    private bool m_is_dragging = false;
    private ScrollRect m_scroll_rect;
    private RectTransform m_tr;
    private RectTransform m_content_tr;

    private float m_page_width;

    public RectTransform[] m_pages;
    private int m_page_count;

    private Vector2 m_target_pos;

    private const float SNAPPING_SPEED = 20.0f;

    // Use this for initialization
    void Start () {
        m_tr = GetComponent<RectTransform>();
        m_scroll_rect = GetComponent<ScrollRect>();
        m_content_tr = m_scroll_rect.content;

        m_page_width = m_tr.rect.width;
        // Debug.Log(m_page_width);
        m_page_count = m_pages.Length;

        m_target_pos = m_content_tr.anchoredPosition;
        // Debug.Log(m_target_pos);
    }
	
	// Update is called once per frame
	void Update () {
		if(!m_is_dragging)
        {
            float distance_to_target = Mathf.Abs(Vector2.Distance(m_content_tr.anchoredPosition, m_target_pos));

            if(distance_to_target > 0)
            {
                m_content_tr.anchoredPosition = Vector2.Lerp(m_content_tr.anchoredPosition, m_target_pos, SNAPPING_SPEED * Time.deltaTime);
            }
        }
    }

    public void OnBeginDrag()
    {
        m_is_dragging = true;
    }

    public void OnEndDrag()
    {
        m_is_dragging = false;

        // Debug.Log(m_content_tr.anchoredPosition.x);
        float content_x = m_content_tr.anchoredPosition.x;
        // Debug.Log(content_x);
        content_x *= -1;
        content_x += m_page_width / 2;
        content_x /= m_page_width;

        int page_index_to_snap = (int)Mathf.Clamp(Mathf.Floor(content_x), 0f, (float)(m_page_count - 1));
        // Debug.Log(page_index_to_snap);

        m_target_pos = new Vector2(-1f * m_page_width * page_index_to_snap, m_content_tr.anchoredPosition.y);
    }
}
