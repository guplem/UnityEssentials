using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectTransformExtensions
  {
    /// <summary>
    /// Get the corners of the RectTransform in world space.
    /// <para>This method could behave weirdly in some cases.</para>
    /// </summary>
    /// <returns>A Rect defining the X and Y position, width and height of the RectTransform</returns>
    public static Rect GetWorldRect(this RectTransform RT)
    {
      // Be aware: a standard Rect has the position as the upper-left corner,
      // and I think the Unity UI stuff somehow repurposes this to instead point to the
      // lower-left. I'm not 100% sure on this, but I've had some unexplained wierdnesses.
      Vector3[] corners = new Vector3[4];
      RT.GetWorldCorners(corners);
      Vector2 Size = new Vector2(corners[2].x - corners[1].x, corners[1].y - corners[0].y);
      return new Rect(new Vector2(corners[1].x, -corners[1].y), Size);
    }

    //Helper function for saving the anchors as one, instead of playing with both corners.

    public static MinMax01 GetAnchors(this RectTransform RT)
    {
      return new MinMax01(RT.anchorMin, RT.anchorMax);
    }

    //Helper function to restore the anchors as above.

    public static void SetAnchors(this RectTransform RT, MinMax01 anchors)
    {
      RT.anchorMin = anchors.min;
      RT.anchorMax = anchors.max;
    }

    //Returns the parent of the given object as a RectTransform.

    public static RectTransform GetParent(this RectTransform RT)
    {
      return RT.parent as RectTransform;
    }

    //Gets the width, height, or both.  Since these are wrappers to properties, these
    // are likely quite slower than the alternative. These are included for 
    // consistency's sake.

    public static float GetWidth(this RectTransform RT)
    {
      return RT.rect.width;
    }
    public static float GetHeight(this RectTransform RT)
    {
      return RT.rect.height;
    }

    public static Vector2 GetSize(this RectTransform RT)
    {
      return new Vector2(RT.GetWidth(), RT.GetHeight());
    }

    //Sets the width, height, or both.

    public static void SetWidth(this RectTransform RT, float width)
    {
      RT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    public static void SetHeight(this RectTransform RT, float height)
    {
      RT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    public static void SetSize(this RectTransform RT, float width, float height)
    {
      RT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
      RT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    public static void SetSize(this RectTransform RT, Vector2 size)
    {
      RT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
      RT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
    }

    //There used to be SetPos functions here.  These have been removed due to the inclusion
    // of the more clear Move and MoveFrom function family.

    //These four functions actually return the center of the edge mentioned, so
    // GetLeft gives you the center-left point, etc.  

    public static Vector2 GetLeft(this RectTransform RT)
    {
      return new Vector2(RT.offsetMin.x, RT.anchoredPosition.y);
    }

    public static Vector2 GetRight(this RectTransform RT)
    {
      return new Vector2(RT.offsetMax.x, RT.anchoredPosition.y);
    }

    public static Vector2 GetTop(this RectTransform RT)
    {
      return new Vector2(RT.anchoredPosition.x, RT.offsetMax.y);
    }

    public static Vector2 GetBottom(this RectTransform RT)
    {
      return new Vector2(RT.anchoredPosition.x, RT.offsetMin.y);
    }

    //Similar to setting the "Left" etc variables in the inspector.  Unlike the inspector, these
    // can be used regardless of anchor position.  Be warned, there's a reason the functionality
    // is hidden in the editor, as the behavior is unintuitive when adjusting the parent's rect.
    // If you're calling these every frame or otherwise updating frequently, shouldn't be a problem, though.
    //
    //Keep in mind that these functions all use standard directions when determining positioning; this means 
    // that unlike the inspector, positive ALWAYS means to the right/top, and negative ALWAYS means to the left/
    // bottom.  If you want true inspector functionality, use Left() and so on, below.
    //
    //E.g., SetLeftEdge(-10) will turn
    /*
        .__________.
        |          |
        |          |
        |   [ ]    |
        |          |
        |__________|
    
            into
        .__________.
        |          |
        |          |
      [       ]    |
        |          |
        |__________|

      [ ] is the RectTransform, the bigger square is the parent
    */

    public static void SetLeft(this RectTransform RT, float left)
    {
      float xmin = RT.GetParent().rect.xMin;
      float anchorFactor = RT.anchorMin.x * 2 - 1;

      RT.offsetMin = new Vector2(xmin + (xmin * anchorFactor) + left, RT.offsetMin.y);
    }

    public static void SetRight(this RectTransform RT, float right)
    {
      float xmax = RT.GetParent().rect.xMax;
      float anchorFactor = RT.anchorMax.x * 2 - 1;

      RT.offsetMax = new Vector2(xmax - (xmax * anchorFactor) + right, RT.offsetMax.y);
    }

    public static void SetTop(this RectTransform RT, float top)
    {
      float ymax = RT.GetParent().rect.yMax;
      float anchorFactor = RT.anchorMax.y * 2 - 1;

      RT.offsetMax = new Vector2(RT.offsetMax.x, ymax - (ymax * anchorFactor) + top);
    }

    public static void SetBottom(this RectTransform RT, float bottom)
    {
      float ymin = RT.GetParent().rect.yMin;
      float anchorFactor = RT.anchorMin.y * 2 - 1;

      RT.offsetMin = new Vector2(RT.offsetMin.x, ymin + (ymin * anchorFactor) + bottom);
    }

    //Truly matches the functionality of the "Left" etc property in the inspector. This means that
    // Right(10) will actually move the right edge to 10 units from the LEFT of the parent's right
    // edge.  In other words, all coordinates are "inside": they measure distance from the parent's
    // edge to the inside of the parent.

    public static void Left(this RectTransform RT, float left)
    {
      RT.SetLeft(left);
    }

    public static void Right(this RectTransform RT, float right)
    {
      RT.SetRight(-right);
    }

    public static void Top(this RectTransform RT, float top)
    {
      RT.SetTop(-top);
    }

    public static void Bottom(this RectTransform RT, float bottom)
    {
      RT.SetRight(bottom);
    }

    //Repositions the requested edge relative to the passed anchor. This lets you set e.g.
    // the left edge relative to the parent's right edge, etc.
    //
    //While this is intended for use with the default anchors, really arbitrary points
    // can be used.

    public static void SetLeftFrom(this RectTransform RT, MinMax01 anchor, float left)
    {
      Vector2 origin = RT.AnchorToParentSpace(anchor.min - RT.anchorMin);

      RT.offsetMin = new Vector2(origin.x + left, RT.offsetMin.y);
    }

    public static void SetRightFrom(this RectTransform RT, MinMax01 anchor, float right)
    {
      Vector2 origin = RT.AnchorToParentSpace(anchor.max - RT.anchorMax);

      RT.offsetMax = new Vector2(origin.x + right, RT.offsetMax.y);
    }

    public static void SetTopFrom(this RectTransform RT, MinMax01 anchor, float top)
    {
      Vector2 origin = RT.AnchorToParentSpace(anchor.max - RT.anchorMax);

      RT.offsetMax = new Vector2(RT.offsetMax.x, origin.y + top);
    }

    public static void SetBottomFrom(this RectTransform RT, MinMax01 anchor, float bottom)
    {
      Vector2 origin = RT.AnchorToParentSpace(anchor.min - RT.anchorMin);

      RT.offsetMin = new Vector2(RT.offsetMin.x, origin.y + bottom);
    }

    //Moves the edge to the requested position relative to the current position.  
    // NOTE:  using these functions repeatedly will result in unintuitive
    // behavior, since the anchored position is getting changed with each call.  
 
    public static void SetRelativeLeft(this RectTransform RT, float left)
    {
      RT.offsetMin = new Vector2(RT.anchoredPosition.x + left, RT.offsetMin.y);
    }

    public static void SetRelativeRight(this RectTransform RT, float right)
    {
      RT.offsetMax = new Vector2(RT.anchoredPosition.x + right, RT.offsetMax.y);
    }

    public static void SetRelativeTop(this RectTransform RT, float top)
    {
      RT.offsetMax = new Vector2(RT.offsetMax.x, RT.anchoredPosition.y + top);
    }

    public static void SetRelativeBottom(this RectTransform RT, float bottom)
    {
      RT.offsetMin = new Vector2(RT.offsetMin.x, RT.anchoredPosition.y + bottom);
    }

    //Sets the position of the RectTransform relative to the parent's Left etc side,
    // regardless of anchor setting. 
    //E.g., MoveLeft(0) will look like this:
    /*
        .__________.
        |          |
        |          |
       [|]         |
        |          |
        |__________|
    */

    public static void MoveLeft(this RectTransform RT, float left = 0)
    {
      float xmin = RT.GetParent().rect.xMin;
      float center = RT.anchorMax.x - RT.anchorMin.x;
      float anchorFactor = RT.anchorMax.x * 2 - 1;

      RT.anchoredPosition = new Vector2(xmin + (xmin * anchorFactor) + left - (center * xmin), RT.anchoredPosition.y);
    }

    public static void MoveRight(this RectTransform RT, float right = 0)
    {
      float xmax = RT.GetParent().rect.xMax;
      float center = RT.anchorMax.x - RT.anchorMin.x;
      float anchorFactor = RT.anchorMax.x * 2 - 1;

      RT.anchoredPosition = new Vector2(xmax - (xmax * anchorFactor) - right + (center * xmax), RT.anchoredPosition.y);
    }

    public static void MoveTop(this RectTransform RT, float top = 0)
    {
      float ymax = RT.GetParent().rect.yMax;
      float center = RT.anchorMax.y - RT.anchorMin.y;
      float anchorFactor = RT.anchorMax.y * 2 - 1;

      RT.anchoredPosition = new Vector2(RT.anchoredPosition.x, ymax - (ymax * anchorFactor) - top + (center * ymax));
    }

    public static void MoveBottom(this RectTransform RT, float bottom = 0)
    {
      float ymin = RT.GetParent().rect.yMin;
      float center = RT.anchorMax.y - RT.anchorMin.y;
      float anchorFactor = RT.anchorMax.y * 2 - 1;

      RT.anchoredPosition = new Vector2(RT.anchoredPosition.x, ymin + (ymin * anchorFactor) + bottom - (center * ymin));
    }

    //Moves the RectTransform to align the child left edge with the parent left edge, etc.  
    //E.g., MoveLeftInside(0) will look like this:
    /*
        .__________.
        |          |
        |          |
        [ ]        |
        |          |
        |__________|
    */

    public static void MoveLeftInside(this RectTransform RT, float left = 0)
    {
      RT.MoveLeft(left + RT.GetWidth() / 2);
    }

    public static void MoveRightInside(this RectTransform RT, float right = 0)
    {
      RT.MoveRight(right + RT.GetWidth() / 2);
    }

    public static void MoveTopInside(this RectTransform RT, float top = 0)
    {
      RT.MoveTop(top + RT.GetHeight() / 2);
    }

    public static void MoveBottomInside(this RectTransform RT, float bottom = 0)
    {
      RT.MoveBottom(bottom + RT.GetHeight() / 2);
    }

    //Moves the RectTransform to align the child right edge with the parent left edge, etc
    //E.g., MoveLeftOutside(0) will look like this:
    /*
        .__________.
        |          |
        |          |
      [ ]          |
        |          |
        |__________|
    */

    public static void MoveLeftOutside(this RectTransform RT, float left = 0)
    {
      RT.MoveLeft(left - RT.GetWidth() / 2);
    }

    public static void MoveRightOutside(this RectTransform RT, float right = 0)
    {
      RT.MoveRight(right - RT.GetWidth() / 2);
    }

    public static void MoveTopOutside(this RectTransform RT, float top = 0)
    {
      RT.MoveTop(top - RT.GetHeight() / 2);
    }

    public static void MoveBottomOutside(this RectTransform RT, float bottom = 0)
    {
      RT.MoveBottom(bottom - RT.GetHeight() / 2);
    }

    //Moves the RectTransform to the given point in parent space, considering (0, 0)
    // to be the parent's lower-left corner.

    public static void Move(this RectTransform RT, float x, float y)
    {
      RT.MoveLeft(x);
      RT.MoveBottom(y);
    }

    public static void Move(this RectTransform RT, Vector2 point)
    {
      RT.MoveLeft(point.x);
      RT.MoveBottom(point.y);
    }

    //Moves the RectTransform relative to the parent's lower-le1ft corner, respecting
    // the RT's width and height.  See MoveLeftInside.

    public static void MoveInside(this RectTransform RT, float x, float y)
    {
      RT.MoveLeftInside(x);
      RT.MoveBottomInside(y);
    }

    public static void MoveInside(this RectTransform RT, Vector2 point)
    {
      RT.MoveLeftInside(point.x);
      RT.MoveBottomInside(point.y);
    }

    //Moves the RectTransform relative to the parent's lower-left corner, respecting
    // the RT's width and height.  See MoveLeftOutside.

    public static void MoveOutside(this RectTransform RT, float x, float y)
    {
      RT.MoveLeftOutside(x);
      RT.MoveBottomOutside(y);
    }

    public static void MoveOutside(this RectTransform RT, Vector2 point)
    {
      RT.MoveLeftOutside(point.x);
      RT.MoveBottomOutside(point.y);
    }

    //Moves the RectTransform relative to an arbitrary anchor point.  This is effectively 
    // like setting the anchor, then moving, then setting it back, but does so without
    // potentially getting in the way of anything else.

    public static void MoveFrom(this RectTransform RT, MinMax01 anchor, Vector2 point)
    {
      RT.MoveFrom(anchor, point.x, point.y);
    }

    public static void MoveFrom(this RectTransform RT, MinMax01 anchor, float x, float y)
    {
      Vector2 origin = RT.AnchorToParentSpace(AnchorOrigin(anchor) - RT.AnchorOrigin());
      RT.anchoredPosition = new Vector2(origin.x + x, origin.y + y);
    }

    //Translates a point on the parent's frame of reference, with (0, 0) being the parent's 
    // lower-left hand corner, into the same point relative to the RectTransform's current
    // anchor. 
    
    public static Vector2 ParentToChildSpace(this RectTransform RT, Vector2 point)
    {
      return RT.ParentToChildSpace(point.x, point.y);
    }

    public static Vector2 ParentToChildSpace(this RectTransform RT, float x, float y)
    {
      float xmin = RT.GetParent().rect.xMin;
      float ymin = RT.GetParent().rect.yMin;
      float anchorFactorX = RT.anchorMin.x * 2 - 1;
      float anchorFactorY = RT.anchorMin.y * 2 - 1;

      return new Vector2(xmin + (xmin * anchorFactorX) + x, ymin + (ymin * anchorFactorY) + y);
    }


    //Translates a point (presumably the RectTransform's anchoredPosition) into the same
    // point on the parent's frame of reference, with (0, 0) being the parent's lower-left
    // hand corner.
    
    public static Vector2 ChildToParentSpace(this RectTransform RT, float x, float y)
    {
      return RT.AnchorOriginParent() + new Vector2(x, y);
    }

    public static Vector2 ChildToParentSpace(this RectTransform RT, Vector2 point)
    {
      return RT.AnchorOriginParent() + point;
    }

    //Normalizes a point associated with the parent object into "Anchor Space", which is
    // to say, (0, 0) represents the parent's lower-left-hand corner, and (1, 1) represents
    // the upper-right-hand.
    
    public static Vector2 ParentToAnchorSpace(this RectTransform RT, Vector2 point)
    {
      return RT.ParentToAnchorSpace(point.x, point.y);
    }

    public static Vector2 ParentToAnchorSpace(this RectTransform RT, float x, float y)
    {
      Rect parent = RT.GetParent().rect;
      if (parent.width != 0)
        x /= parent.width;
      else
        x = 0;

      if (parent.height != 0)
        y /= parent.height;
      else
        y = 0;

      return new Vector2(x, y);
    }

    //Translates a normalized "Anchor Space" coordinate into a real point on the parent's
    // reference system.
    
    public static Vector2 AnchorToParentSpace(this RectTransform RT, float x, float y)
    {
      return new Vector2(x * RT.GetParent().rect.width, y * RT.GetParent().rect.height);
    }

    public static Vector2 AnchorToParentSpace(this RectTransform RT, Vector2 point)
    {
      return new Vector2(point.x * RT.GetParent().rect.width, point.y * RT.GetParent().rect.height);
    }


    //Since both anchors usually sit on the same coordinate, it can be easy to treat them
    // as a single point.  This will however lead to problems whenever they are apart, such as
    // when any of the Stretch anchors are used.  This calculates the center of the rectangle
    // the two points represent, which is the origin that a RectTransform's anchoredPosition
    // is an offset of.

    public static Vector2 AnchorOrigin(this RectTransform RT)
    {
      return AnchorOrigin(RT.GetAnchors());
    }

    public static Vector2 AnchorOrigin(MinMax01 anchor)
    {
      float x = anchor.min.x + (anchor.max.x - anchor.min.x) / 2;
      float y = anchor.min.y + (anchor.max.y - anchor.min.y) / 2;

      return new Vector2(x, y);
    }

    //Translates a RectTransform's anchor origin into Parent space, so you don't have to pass
    // the result of AnchorOrigin() to AnchorToParentSpace().

    public static Vector2 AnchorOriginParent(this RectTransform RT)
    {
      return Vector2.Scale(RT.AnchorOrigin(), new Vector2(RT.GetParent().rect.width, RT.GetParent().rect.height));
    }

    //Helper to get the top-most-level canvas that this RectTransform is a child of.

    public static Canvas GetRootCanvas(this RectTransform RT)
    {
      Canvas rootCanvas = RT.GetComponentInParent<Canvas>();

      while (!rootCanvas.isRootCanvas)
        rootCanvas = rootCanvas.transform.parent.GetComponentInParent<Canvas>();

      return rootCanvas;
    }
  }



// OLD
/*public static class RectTransformExtensions
{
    /// <summary>
    /// Sets the distance from the anchor at the left of the rect transform left edge.
    /// </summary>
    /// <param name="distance">The distance between the RectTransform and the anchor at the left.</param>
    public static void SetLeft(this RectTransform rt, float distance)
    {
        rt.offsetMin = new Vector2(distance, rt.offsetMin.y);
    }
    /// <summary>
    /// Sets the distance from the anchor at the right of the rect transform right edge.
    /// </summary>
    /// <param name="distance">The distance between the RectTransform and the anchor at the right.</param>
    public static void SetRight(this RectTransform rt, float distance)
    {
        rt.offsetMax = new Vector2(-distance, rt.offsetMax.y);
    }
    /// <summary>
    /// Sets the distance from the anchor at the top of the rect transform top edge.
    /// </summary>
    /// <param name="distance">The distance between the RectTransform and the anchor at the top.</param>
    public static void SetTop(this RectTransform rt, float distance)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -distance);
    }
    /// <summary>
    /// Sets the distance from the anchor at the bottom of the rect transform bottom edge.
    /// </summary>
    /// <param name="distance">The distance between the RectTransform and the anchor at the bottom.</param>
    public static void SetBottom(this RectTransform rt, float distance)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, distance);
    }
    
    /// <summary>
    /// Gets the distance from the anchor at the left of the rect transform left edge.
    /// </summary>
    /// <returns>The distance from the anchor at the left of the rect transform left edge.</returns>
    public static float GetLeft(this RectTransform rt)
    {
        return rt.offsetMin.x;
    }
    /// <summary>
    /// Gets the distance from the anchor at the right of the rect transform right edge.
    /// </summary>
    /// <returns>The distance from the anchor at the right of the rect transform right edge.</returns>
    public static float GetRight(this RectTransform rt)
    {
        return -rt.offsetMax.x;
    }
    /// <summary>
    /// Gets the distance from the anchor at the top of the rect transform top edge.
    /// </summary>
    /// <returns>The distance from the anchor at the top of the rect transform top edge.</returns>
    public static float GetTop(this RectTransform rt)
    {
        return -rt.offsetMax.y;
    }
    /// <summary>
    /// Gets the distance from the anchor at the bottom of the rect transform bottom edge.
    /// </summary>
    /// <returns>The distance from the anchor at the bottom of the rect transform bottom edge.</returns>
    public static float GetBottom(this RectTransform rt)
    {
        return rt.offsetMin.y;
    }
    
}*/
