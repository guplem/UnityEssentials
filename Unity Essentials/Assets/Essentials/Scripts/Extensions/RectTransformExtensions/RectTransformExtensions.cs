/*
The MIT License (MIT)

Copyright (c) 2015 Christian 'ketura' McCarty

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


// Thanks Christian 'ketura' McCarty for your code, which I found extremely useful and a must-have.


namespace UnityEngine
{
  
  /// <summary>
  /// Extensions for the RectTransform component
  /// </summary>
  public static class RectTransformExtensions
  {
    /// <summary>
    /// Get the corners of the RectTransform in world space.
    /// <para>This method could behave weirdly in some cases.</para>
    /// </summary>
    /// <returns>A Rect defining the X and Y position, width and height of the RectTransform.</returns>
    public static Rect GetWorldRect(this RectTransform rt)
    {
      // Be aware: a standard Rect has the position as the upper-left corner,
      // and I think the Unity UI stuff somehow repurposes this to instead point to the
      // lower-left. I'm not 100% sure on this, but I've had some unexplained wierdnesses.
      Vector3[] corners = new Vector3[4];
      rt.GetWorldCorners(corners);
      Vector2 size = new Vector2(corners[2].x - corners[1].x, corners[1].y - corners[0].y);
      return new Rect(new Vector2(corners[1].x, -corners[1].y), size);
    }
    
    /// <summary>
    /// The normalized position in the parent RectTransform that the corners is anchored to.
    /// </summary>
    /// <returns>A MinMax01 defining the normalized position in the parent RectTransform that the corners is anchored to.</returns>     
    public static MinMax01 GetAnchors(this RectTransform rt)
    {
      return new MinMax01(rt.anchorMin, rt.anchorMax);
    }

    
    /// <summary>
    /// Sets the normalized position in the parent RectTransform that the corners is anchored to.
    /// </summary>    
    public static void SetAnchors(this RectTransform rt, MinMax01 anchors)
    {
      rt.anchorMin = anchors.min;
      rt.anchorMax = anchors.max;
    }

    /// <summary>
    /// The RecTransform parent of the RectTransform.
    /// </summary>
    /// <returns>The RecTransform parent of the RectTransform.</returns>   
    public static RectTransform GetParent(this RectTransform rt)
    {
      return rt.parent as RectTransform;
    }
    
    /// <summary>
    /// The width of the RectTransform, measured from the X position.
    /// </summary>
    /// <returns>The width of the RectTransform, measured from the X position.</returns>   
    public static float GetWidth(this RectTransform rt)
    {
      return rt.rect.width;
    }
    
    /// <summary>
    /// The height of the RectTransform, measured from the Y position.
    /// </summary>
    /// <returns>The height of the RectTransform, measured from the Y position.</returns>   
    public static float GetHeight(this RectTransform rt)
    {
      return rt.rect.height;
    }

    /// <summary>
    /// The width and the height of the RectTransform, measured from the (X,Y) position.
    /// </summary>
    /// <returns>The width and the height of the RectTransform, measured from the (X,Y) position.</returns>   
    public static Vector2 GetSize(this RectTransform rt)
    {
      return new Vector2(rt.GetWidth(), rt.GetHeight());
    }

    /// <summary>
    /// Sets the width of the RectTransform keeping the current anchors.
    /// </summary>
    public static void SetWidth(this RectTransform rt, float width)
    {
      rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    /// <summary>
    /// Sets the height of the RectTransform keeping the current anchors.
    /// </summary>
    public static void SetHeight(this RectTransform rt, float height)
    {
      rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    /// <summary>
    /// Sets the width and the height of the RectTransform keeping the current anchors.
    /// </summary>
    /// <param name="width">The desired width of the RectTransform</param>
    /// <param name="height">The desired height of the RectTransform</param>
    public static void SetSize(this RectTransform rt, float width, float height)
    {
      rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
      rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    /// <summary>
    /// Sets the width and the height of the RectTransform keeping the current anchors.
    /// </summary>
    /// <param name="size">The desired width (x) and height (y) of the RectTransform.</param>
    public static void SetSize(this RectTransform rt, Vector2 size)
    {
      rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
      rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
    }

    /// <summary>
    /// The center of the left edge.
    /// </summary>
    /// <returns>The center of the left edge.</returns>
    public static Vector2 GetLeft(this RectTransform rt)
    {
      return new Vector2(rt.offsetMin.x, rt.anchoredPosition.y);
    }
    
    /// <summary>
    /// The center of the right edge.
    /// </summary>
    /// <returns>The center of the right edge.</returns>
    public static Vector2 GetRight(this RectTransform rt)
    {
      return new Vector2(rt.offsetMax.x, rt.anchoredPosition.y);
    }

    /// <summary>
    /// The center of the top edge.
    /// </summary>
    /// <returns>The center of the top edge.</returns>
    public static Vector2 GetTop(this RectTransform rt)
    {
      return new Vector2(rt.anchoredPosition.x, rt.offsetMax.y);
    }

    /// <summary>
    /// The center of the bottom edge.
    /// </summary>
    /// <returns>The center of the bottom edge.</returns>
    public static Vector2 GetBottom(this RectTransform rt)
    {
      return new Vector2(rt.anchoredPosition.x, rt.offsetMin.y);
    }

    
    // Set[edgeName](float) is similar to setting the "Left" etc variables in the inspector.  Unlike the inspector, these
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
    /// <summary>
    /// Sets the distance of the distance from the left edge to the parent's left edge regardless of the anchor position.
    /// </summary>
    /// <param name="distance"></param>
    public static void SetLeft(this RectTransform rt, float distance)
    {
      float xmin = rt.GetParent().rect.xMin;
      float anchorFactor = rt.anchorMin.x * 2 - 1;

      rt.offsetMin = new Vector2(xmin + (xmin * anchorFactor) + distance, rt.offsetMin.y);
    }
    /// <summary>
    /// Sets the distance of the distance from the left edge to the parent's left edge regardless of the anchor position.
    /// </summary>
    /// <param name="distance"></param>
    public static void SetRight(this RectTransform rt, float distance)
    {
      float xmax = rt.GetParent().rect.xMax;
      float anchorFactor = rt.anchorMax.x * 2 - 1;

      rt.offsetMax = new Vector2(xmax - (xmax * anchorFactor) + distance, rt.offsetMax.y);
    }
    /// <summary>
    /// Sets the distance of the distance from the top edge to the parent's top edge regardless of the anchor position.
    /// </summary>
    /// <param name="distance"></param>
    public static void SetTop(this RectTransform rt, float top)
    {
      float ymax = rt.GetParent().rect.yMax;
      float anchorFactor = rt.anchorMax.y * 2 - 1;

      rt.offsetMax = new Vector2(rt.offsetMax.x, ymax - (ymax * anchorFactor) + top);
    }
    /// <summary>
    /// Sets the distance of the distance from the bottom edge to the parent's bottom edge regardless of the anchor position.
    /// </summary>
    /// <param name="distance"></param>
    public static void SetBottom(this RectTransform rt, float distance)
    {
      float ymin = rt.GetParent().rect.yMin;
      float anchorFactor = rt.anchorMin.y * 2 - 1;

      rt.offsetMin = new Vector2(rt.offsetMin.x, ymin + (ymin * anchorFactor) + distance);
    }

    // Truly matches the functionality of the "Left" etc property in the inspector. This means that
    // Right(10) will actually move the right edge to 10 units from the LEFT of the parent's right edge.
    // In other words, all coordinates are "inside": they measure distance from the parent's edge to the inside of the parent.
    /// <summary>
    /// Moves the edge to a distance towards the center from the same edge of the parent.
    /// </summary>
    /// <param name="distance"></param>
    public static void Left(this RectTransform rt, float distance)
    {
      rt.SetLeft(distance);
    }
    /// <summary>
    /// Moves the edge to a distance towards the center from the same edge of the parent.
    /// </summary>
    /// <param name="distance"></param>
    public static void Right(this RectTransform rt, float distance)
    {
      rt.SetRight(-distance);
    }
    /// <summary>
    /// Moves the edge to a distance towards the center from the same edge of the parent.
    /// </summary>
    /// <param name="distance"></param>
    public static void Top(this RectTransform rt, float distance)
    {
      rt.SetTop(-distance);
    }
    /// <summary>
    /// Moves the edge to a distance towards the center from the same edge of the parent.
    /// </summary>
    /// <param name="distance"></param>
    public static void Bottom(this RectTransform rt, float distance)
    {
      rt.SetRight(distance);
    }


    /// <summary>
    /// Repositions the requested edge relative to the passed anchor.
    /// </summary>
    /// <param name="anchor">The anchor to get the relative from.</param>
    /// <param name="distance">The distance to be moved to.</param>
    public static void SetLeftFrom(this RectTransform rt, MinMax01 anchor, float distance)
    {
      Vector2 origin = rt.AnchorToParentSpace(anchor.min - rt.anchorMin);

      rt.offsetMin = new Vector2(origin.x + distance, rt.offsetMin.y);
    }
    /// <summary>
    /// Repositions the requested edge relative to the passed anchor.
    /// </summary>
    /// <param name="anchor">The anchor to get the relative from.</param>
    /// <param name="distance">The distance to be moved to.</param>
    public static void SetRightFrom(this RectTransform rt, MinMax01 anchor, float distance)
    {
      Vector2 origin = rt.AnchorToParentSpace(anchor.max - rt.anchorMax);

      rt.offsetMax = new Vector2(origin.x + distance, rt.offsetMax.y);
    }
    /// <summary>
    /// Repositions the requested edge relative to the passed anchor.
    /// </summary>
    /// <param name="anchor">The anchor to get the relative from.</param>
    /// <param name="distance">The distance to be moved to.</param>
    public static void SetTopFrom(this RectTransform rt, MinMax01 anchor, float distance)
    {
      Vector2 origin = rt.AnchorToParentSpace(anchor.max - rt.anchorMax);

      rt.offsetMax = new Vector2(rt.offsetMax.x, origin.y + distance);
    }
    /// <summary>
    /// Repositions the requested edge relative to the passed anchor.
    /// </summary>
    /// <param name="anchor">The anchor to get the relative from.</param>
    /// <param name="distance">The distance to be moved to.</param>
    public static void SetBottomFrom(this RectTransform rt, MinMax01 anchor, float distance)
    {
      Vector2 origin = rt.AnchorToParentSpace(anchor.min - rt.anchorMin);

      rt.offsetMin = new Vector2(rt.offsetMin.x, origin.y + distance);
    }


    
    /// <summary>
    /// Moves the edge to the requested position relative to the current position.
    /// <para>Using these functions repeatedly will result in unintuitive behavior, since the anchored position is getting changed with each call.</para>
    /// </summary>
    /// <param name="rt"></param>
    /// <param name="distance">The distance you to displace the left edge.</param>
    public static void SetRelativeLeft(this RectTransform rt, float distance)
    {
      rt.offsetMin = new Vector2(rt.anchoredPosition.x + distance, rt.offsetMin.y);
    }
    /// <summary>
    /// Moves the edge to the requested position relative to the current position.
    /// <para>Using these functions repeatedly will result in unintuitive behavior, since the anchored position is getting changed with each call.</para>
    /// </summary>
    /// <param name="rt"></param>
    /// <param name="distance">The distance you to displace the right edge.</param>
    public static void SetRelativeRight(this RectTransform rt, float distance)
    {
      rt.offsetMax = new Vector2(rt.anchoredPosition.x + distance, rt.offsetMax.y);
    }
    /// <summary>
    /// Moves the edge to the requested position relative to the current position.
    /// <para>Using these functions repeatedly will result in unintuitive behavior, since the anchored position is getting changed with each call.</para>
    /// </summary>
    /// <param name="rt"></param>
    /// <param name="distance">The distance you to displace the top edge.</param>
    public static void SetRelativeTop(this RectTransform rt, float distance)
    {
      rt.offsetMax = new Vector2(rt.offsetMax.x, rt.anchoredPosition.y + distance);
    }
    /// <summary>
    /// Moves the edge to the requested position relative to the current position.
    /// <para>Using these functions repeatedly will result in unintuitive behavior, since the anchored position is getting changed with each call.</para>
    /// </summary>
    /// <param name="rt"></param>
    /// <param name="distance">The distance you to displace the bottom edge.</param>
    public static void SetRelativeBottom(this RectTransform rt, float distance)
    {
      rt.offsetMin = new Vector2(rt.offsetMin.x, rt.anchoredPosition.y + distance);
    }
    
    
    
    //E.g., MoveLeft(0) will look like this:
    /*
        .__________.
        |          |
        |          |
       [|]         |
        |          |
        |__________|
    */
    /// <summary>
    /// Sets the position of the RectTransform relative to the parent's Left side, regardless of anchor setting. 
    /// </summary>
    /// <param name="left">Sets the position of the RectTransform relative to the parent's Left side.</param>
    public static void MoveLeft(this RectTransform rt, float left = 0)
    {
      float xmin = rt.GetParent().rect.xMin;
      float center = rt.anchorMax.x - rt.anchorMin.x;
      float anchorFactor = rt.anchorMax.x * 2 - 1;
      rt.anchoredPosition = new Vector2(xmin + (xmin * anchorFactor) + left - (center * xmin), rt.anchoredPosition.y);
    }
    /// <summary>
    /// Sets the position of the RectTransform relative to the parent's Right side, regardless of anchor setting. 
    /// </summary>
    /// <param name="right">Sets the position of the RectTransform relative to the parent's Right side.</param>
    public static void MoveRight(this RectTransform rt, float right = 0)
    {
      float xmax = rt.GetParent().rect.xMax;
      float center = rt.anchorMax.x - rt.anchorMin.x;
      float anchorFactor = rt.anchorMax.x * 2 - 1;
      rt.anchoredPosition = new Vector2(xmax - (xmax * anchorFactor) - right + (center * xmax), rt.anchoredPosition.y);
    }
    /// <summary>
    /// Sets the position of the RectTransform relative to the parent's Top side, regardless of anchor setting. 
    /// </summary>
    /// <param name="top">Sets the position of the RectTransform relative to the parent's Top side.</param>
    public static void MoveTop(this RectTransform rt, float top = 0)
    {
      float ymax = rt.GetParent().rect.yMax;
      float center = rt.anchorMax.y - rt.anchorMin.y;
      float anchorFactor = rt.anchorMax.y * 2 - 1;
      rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, ymax - (ymax * anchorFactor) - top + (center * ymax));
    }
    /// <summary>
    /// Sets the position of the RectTransform relative to the parent's Bottom side, regardless of anchor setting. 
    /// </summary>
    /// <param name="bottom">Sets the position of the RectTransform relative to the parent's Bottom side.</param>
    public static void MoveBottom(this RectTransform rt, float bottom = 0)
    {
      float ymin = rt.GetParent().rect.yMin;
      float center = rt.anchorMax.y - rt.anchorMin.y;
      float anchorFactor = rt.anchorMax.y * 2 - 1;
      rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, ymin + (ymin * anchorFactor) + bottom - (center * ymin));
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
    /// <summary>
    /// Moves the RectTransform to align the child left edge with the parent left edge.
    /// </summary>
    /// <param name="distance">The distance to the parent left edge.</param>
    public static void MoveLeftInside(this RectTransform rt, float distance = 0)
    {
      rt.MoveLeft(distance + rt.GetWidth() / 2);
    }
    /// <summary>
    /// Moves the RectTransform to align the child left edge with the parent right edge.
    /// </summary>
    /// <param name="distance">The distance to the parent right edge.</param>
    public static void MoveRightInside(this RectTransform rt, float distance = 0)
    {
      rt.MoveRight(distance + rt.GetWidth() / 2);
    }
    /// <summary>
    /// Moves the RectTransform to align the child left edge with the parent left top.
    /// </summary>
    /// <param name="distance">The distance to the parent top edge.</param>
    public static void MoveTopInside(this RectTransform rt, float distance = 0)
    {
      rt.MoveTop(distance + rt.GetHeight() / 2);
    }
    /// <summary>
    /// Moves the RectTransform to align the child left edge with the parent bottom edge.
    /// </summary>
    /// <param name="distance">The distance to the parent bottom edge.</param>
    public static void MoveBottomInside(this RectTransform rt, float distance = 0)
    {
      rt.MoveBottom(distance + rt.GetHeight() / 2);
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
    /// <summary>
    /// Moves the RectTransform to align the right edge with the parent left edge.
    /// </summary>
    /// <param name="rt"></param>
    /// <param name="distance">The distance between the edges</param>
    public static void MoveLeftOutside(this RectTransform rt, float distance = 0)
    {
      rt.MoveLeft(distance - rt.GetWidth() / 2);
    }
    /// <summary>
    /// Moves the RectTransform to align the left edge with the parent right edge.
    /// </summary>
    /// <param name="distance">The distance between the edges</param>
    public static void MoveRightOutside(this RectTransform rt, float distance = 0)
    {
      rt.MoveRight(distance - rt.GetWidth() / 2);
    }
    /// <summary>
    /// Moves the RectTransform to align the bottom edge with the parent top edge.
    /// </summary>
    /// <param name="distance">The distance between the edges</param>
    public static void MoveTopOutside(this RectTransform rt, float distance = 0)
    {
      rt.MoveTop(distance - rt.GetHeight() / 2);
    }
    /// <summary>
    /// Moves the RectTransform to align the top edge with the parent bottom edge.
    /// </summary>
    /// <param name="distance">The distance between the edges</param>
    public static void MoveBottomOutside(this RectTransform rt, float distance = 0)
    {
      rt.MoveBottom(distance - rt.GetHeight() / 2);
    }

    
    /// <summary>
    /// Moves the RectTransform to the given point in parent space, considering (0, 0) to be the parent's lower-left corner.
    /// </summary>
    /// <param name="x">X coordinate of the point to move the RectTransform to.</param>
    /// <param name="y">Y coordinate of the point to move the RectTransform to.</param>
    public static void Move(this RectTransform rt, float x, float y)
    {
      rt.MoveLeft(x);
      rt.MoveBottom(y);
    }
    /// <summary>
    /// Moves the RectTransform to the given point in parent space, considering (0, 0) to be the parent's lower-left corner.
    /// </summary>
    /// <param name="point">The point to move the RectTransform to.</param>
    public static void Move(this RectTransform rt, Vector2 point)
    {
      rt.MoveLeft(point.x);
      rt.MoveBottom(point.y);
    }
    
    
    /// <summary>
    /// Moves the RectTransform relative to the parent's lower-le1ft corner, respecting the RT's width and height.
    /// <para>See MoveLeftInside for more information.</para>
    /// </summary>
    /// <param name="x">X coordinate of the point to move the RectTransform to.</param>
    /// <param name="y">Y coordinate of the point to move the RectTransform to.</param>
    public static void MoveInside(this RectTransform rt, float x, float y)
    {
      rt.MoveLeftInside(x);
      rt.MoveBottomInside(y);
    }
    /// <summary>
    /// Moves the RectTransform relative to the parent's lower-le1ft corner, respecting the RT's width and height.
    /// <para>See MoveLeftInside for more information.</para>
    /// </summary>
    /// <param name="point">The point to move the RectTransform to.</param>
    public static void MoveInside(this RectTransform rt, Vector2 point)
    {
      rt.MoveLeftInside(point.x);
      rt.MoveBottomInside(point.y);
    }
    
    
    /// <summary>
    /// Moves the RectTransform relative to the parent's lower-left corner, respecting the RT's width and height.
    /// <para>See MoveLeftOutside for more information.</para>
    /// </summary>
    /// <param name="x">X coordinate of the point to move the RectTransform to.</param>
    /// <param name="y">Y coordinate of the point to move the RectTransform to.</param>
    public static void MoveOutside(this RectTransform rt, float x, float y)
    {
      rt.MoveLeftOutside(x);
      rt.MoveBottomOutside(y);
    }
    /// <summary>
    /// Moves the RectTransform relative to the parent's lower-left corner, respecting the RT's width and height.
    /// <para>See MoveLeftOutside for more information.</para>
    /// </summary>
    /// <param name="point">The point to move the RectTransform to.</param>
    public static void MoveOutside(this RectTransform rt, Vector2 point)
    {
      rt.MoveLeftOutside(point.x);
      rt.MoveBottomOutside(point.y);
    }

    
    /// <summary>
    /// Moves the RectTransform relative to an arbitrary anchor point.  This is effectively like setting the anchor, then moving, then setting it back, but does so without potentially getting in the way of anything else.
    /// </summary>
    public static void MoveFrom(this RectTransform rt, MinMax01 anchor, Vector2 point)
    {
      rt.MoveFrom(anchor, point.x, point.y);
    }
    /// <summary>
    /// Moves the RectTransform relative to an arbitrary anchor point.  This is effectively like setting the anchor, then moving, then setting it back, but does so without potentially getting in the way of anything else.
    /// </summary>
    /// <param name="rt"></param>
    public static void MoveFrom(this RectTransform rt, MinMax01 anchor, float x, float y)
    {
      Vector2 origin = rt.AnchorToParentSpace(AnchorOrigin(anchor) - rt.AnchorOrigin());
      rt.anchoredPosition = new Vector2(origin.x + x, origin.y + y);
    }

    
    /// <summary>
    /// Translates a point on the parent's frame of reference, with (0, 0) being the parent's lower-left hand corner, into the same point relative to the RectTransform's current anchor. 
    /// </summary>
    /// <param name="point">The point to translate.</param>
    /// <returns>The translated point.</returns>
    public static Vector2 ParentToChildSpace(this RectTransform rt, Vector2 point)
    {
      return rt.ParentToChildSpace(point.x, point.y);
    }
    /// <summary>
    /// Translates a point on the parent's frame of reference, with (0, 0) being the parent's lower-left hand corner, into the same point relative to the RectTransform's current anchor. 
    /// </summary>
    /// <param name="x">X coordinate of the point to translate.</param>
    /// <param name="y">Y coordinate of the point to translate.</param>
    /// <returns>The translated point.</returns>
    public static Vector2 ParentToChildSpace(this RectTransform rt, float x, float y)
    {
      float xmin = rt.GetParent().rect.xMin;
      float ymin = rt.GetParent().rect.yMin;
      float anchorFactorX = rt.anchorMin.x * 2 - 1;
      float anchorFactorY = rt.anchorMin.y * 2 - 1;
      return new Vector2(xmin + (xmin * anchorFactorX) + x, ymin + (ymin * anchorFactorY) + y);
    }


    /// <summary>
    /// Translates a point (presumably the RectTransform's anchoredPosition) into the same point on the parent's frame of reference, with (0, 0) being the parent's lower-left hand corner.
    /// </summary>
    /// <param name="x">X coordinate of the point to translate.</param>
    /// <param name="y">Y coordinate of the point to translate.</param>
    /// <returns>The translated point.</returns>
    public static Vector2 ChildToParentSpace(this RectTransform rt, float x, float y)
    {
      return rt.AnchorOriginParent() + new Vector2(x, y);
    }
    /// <summary>
    /// Translates a point (presumably the RectTransform's anchoredPosition) into the same point on the parent's frame of reference, with (0, 0) being the parent's lower-left hand corner.
    /// </summary>
    /// <param name="point">The point to translate.</param>
    /// <returns>The translated point.</returns>
    public static Vector2 ChildToParentSpace(this RectTransform rt, Vector2 point)
    {
      return rt.AnchorOriginParent() + point;
    }


    /// <summary>
    /// Normalizes a point associated with the parent object into "Anchor Space", which is to say, (0, 0) represents the parent's lower-left-hand corner, and (1, 1) represents the upper-right-hand.
    /// </summary>
    /// <param name="point">The point to normalize.</param>
    /// <returns>The normalized point.</returns>
    public static Vector2 ParentToAnchorSpace(this RectTransform rt, Vector2 point)
    {
      return rt.ParentToAnchorSpace(point.x, point.y);
    }
    /// <summary>
    /// Normalizes a point associated with the parent object into "Anchor Space", which is to say, (0, 0) represents the parent's lower-left-hand corner, and (1, 1) represents the upper-right-hand.
    /// </summary>
    /// <param name="x">X coordinate of the point to normalize.</param>
    /// <param name="y">Y coordinate of the point to normalize.</param>
    /// <returns>The normalized point.</returns>
    public static Vector2 ParentToAnchorSpace(this RectTransform rt, float x, float y)
    {
      Rect parent = rt.GetParent().rect;
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

    
    /// <summary>
    /// Translates a normalized "Anchor Space" coordinate into a real point on the parent's reference system.
    /// </summary>
    /// <param name="x">X coordinate of a normalized point set in "Anchor Space".</param>
    /// <param name="y">Y coordinate of a normalized point set in "Anchor Space".</param>
    /// <returns>The anchor space coordinate as a real point on the parent's reference</returns>
    public static Vector2 AnchorToParentSpace(this RectTransform rt, float x, float y)
    {
      return new Vector2(x * rt.GetParent().rect.width, y * rt.GetParent().rect.height);
    }
    /// <summary>
    /// Translates a normalized "Anchor Space" coordinate into a real point on the parent's reference system.
    /// </summary>
    /// <param name="point">A normalized "Anchor Space" point of a normalized point set in "Anchor Space".</param>
    /// <returns>The anchor space coordinate as a real point on the parent's reference</returns>
    public static Vector2 AnchorToParentSpace(this RectTransform rt, Vector2 point)
    {
      return new Vector2(point.x * rt.GetParent().rect.width, point.y * rt.GetParent().rect.height);
    }


   
    /// <summary>
    /// The center of the rectangle the two anchors represent, which is the origin that a RectTransform's anchoredPosition is an offset of.
    /// </summary>
    /// <returns>The center of the rectangle the two anchors represent.</returns>
    public static Vector2 AnchorOrigin(this RectTransform rt)
    {
      return AnchorOrigin(rt.GetAnchors());
    }
    /// <summary>
    /// The center of the rectangle the two anchors represent, which is the origin that a RectTransform's anchoredPosition is an offset of.
    /// </summary>
    /// <returns>The center of the rectangle the two anchors represent.</returns>
    public static Vector2 AnchorOrigin(MinMax01 anchor)
    {
      float x = anchor.min.x + (anchor.max.x - anchor.min.x) / 2;
      float y = anchor.min.y + (anchor.max.y - anchor.min.y) / 2;

      return new Vector2(x, y);
    }

    /// <summary>
    /// Translates a RectTransform's anchor origin into Parent space.
    /// </summary>
    /// <returns>The anchor origin in parent space.</returns>
    public static Vector2 AnchorOriginParent(this RectTransform rt)
    {
      return Vector2.Scale(rt.AnchorOrigin(), new Vector2(rt.GetParent().rect.width, rt.GetParent().rect.height));
    }

    /// <summary>
    /// Returns the top-most-level canvas that this RectTransform is a child of.
    /// </summary>
    /// <returns>The top-most-level canvas that this RectTransform is a child of, the root canvas.</returns>
    public static Canvas GetRootCanvas(this RectTransform rt)
    {
      Canvas rootCanvas = rt.GetComponentInParent<Canvas>();

      while (!rootCanvas.isRootCanvas)
        rootCanvas = rootCanvas.transform.parent.GetComponentInParent<Canvas>();

      return rootCanvas;
    }

    /// <summary>
    /// Sets the RectTransform position, rotation and scale to the same values in the given RectTransform.
    /// </summary>
    /// <param name="properties">The RectTransform from which the position, rotation and scale will be copied.</param>
    public static void SetProperties(this RectTransform rectTrans, RectTransform properties)
    {
      rectTrans.SetProperties(properties.anchoredPosition, properties.anchoredPosition3D, properties.anchorMax,
        properties.anchorMin, properties.offsetMax, properties.offsetMin, properties.pivot, properties.sizeDelta, properties.transform);
    }

    /// <summary>
    /// Sets the transform properties to the given values.
    /// </summary>
    /// <param name="anchoredPosition">The anchoredPosition to be set in the RectTransform.</param>
    /// <param name="anchoredPosition3D">The anchoredPosition3D to be set in the RectTransform.</param>
    /// <param name="anchorMax">The anchorMax to be set in the RectTransform.</param>
    /// <param name="anchorMin">The anchorMin to be set in the RectTransform.</param>
    /// <param name="offsetMax">The offsetMax to be set in the RectTransform.</param>
    /// <param name="offsetMin">The offsetMin to be set in the RectTransform.</param>
    /// <param name="pivot">The pivot to be set in the RectTransform.</param>
    /// <param name="sizeDelta">The sizeDelta to be set in the RectTransform.</param>
    /// <param name="trans">The transform to copy the scale and rotation from (and the position).</param>
    public static void SetProperties(this RectTransform rectTrans, Vector2 anchoredPosition, Vector3 anchoredPosition3D,
      Vector2 anchorMax, Vector2 anchorMin, Vector2 offsetMax, Vector2 offsetMin, Vector2 pivot, Vector2 sizeDelta, Transform trans = null)
    {
      rectTrans.anchoredPosition = anchoredPosition;
      rectTrans.anchoredPosition3D = anchoredPosition3D;
      rectTrans.anchorMax = anchorMax;
      rectTrans.anchorMin = anchorMin;
      rectTrans.offsetMax = offsetMax;
      rectTrans.offsetMin = offsetMin;
      rectTrans.pivot = pivot;
      rectTrans.sizeDelta = sizeDelta;
      if (trans) rectTrans.transform.SetProperties(trans);
    }

    /// <summary>
    /// Linearly interpolates between two RectTransforms.
    /// <para>When t = 0 returns a. When t = 1 returns b. When t = 0.5 returns the point midway between a and b.</para>
    /// </summary>
    public static void SetLerp(this RectTransform self, RectTransform a, RectTransform b, float t)
    {
      self.SetProperties(
        Vector2.Lerp(a.anchoredPosition, b.anchoredPosition, t),
        Vector3.Lerp(a.anchoredPosition3D, b.anchoredPosition3D, t),
        Vector2.Lerp(a.anchorMax, b.anchorMax, t),
        Vector2.Lerp(a.anchorMin, b.anchorMin, t),
        Vector2.Lerp(a.offsetMax, b.offsetMax, t),
        Vector2.Lerp(a.offsetMin, b.offsetMin, t),
        Vector2.Lerp(a.pivot, b.pivot, t),
        Vector2.Lerp(a.sizeDelta, b.sizeDelta, t)
      );
      self.transform.SetLerp(a.transform, b.transform, t);
    }

  }
}
