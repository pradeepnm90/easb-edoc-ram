module Watir
  class Element
    def drag_drop_on(target, src_offset=0, dst_offset=0)
      assert_target target
      drop_x = target.left_edge_absolute + dst_offset
      drop_y = target.top_edge_absolute + dst_offset
      drag_to(drop_x, drop_y, src_offset)
    end

    def drag_drop_distance(distance_x, distance_y, src_offset=0, dst_offset=0)
      drag_x, drag_y = source_x_y(src_offset)
      drop_x = drag_x + distance_x + dst_offset
      drop_y = drag_y + distance_y + dst_offset
      drag_to(drop_x, drop_y, src_offset)
    end

    def drag_drop_at(drop_x, drop_y, src_offset=0)
      drag_to(drop_x, drop_y, src_offset)
    end

    def drag_drop_below(target, src_offset=0, dst_offset=0)
      assert_target target
      drop_x = target.left_edge_absolute + dst_offset
      drop_y = target.bottom_edge_absolute + 2 + dst_offset
      drag_to(drop_x, drop_y, src_offset)
    end

    def drag_drop_above(target, src_offset=0, dst_offset=0)
      assert_target target
      drop_x = target.left_edge_absolute + dst_offset
      drop_y = target.top_edge_absolute - 2 + dst_offset
      drag_to(drop_x, drop_y, src_offset)
    end

    private

    def drag_to(drop_x, drop_y, src_offset)
      drag_x, drag_y = source_x_y(src_offset)
      WindowsInput.move_mouse(drag_x, drag_y)
      WindowsInput.left_down
      WindowsInput.move_mouse(drop_x, drop_y)
      WindowsInput.left_up
    end

    def source_x_y(src_offset)
      return left_edge_absolute + src_offset, top_edge_absolute + src_offset
    end

    def assert_target(target)
      target.assert_exists
      #target.assert_enabled
    end

    def top_edge
      assert_target self
      ole_object.getBoundingClientRect.top.to_i
    end

    def top_edge_absolute
      top_edge + page_container.document.parentWindow.screenTop.to_i
    end


    #def left_edge
    #  assert_target self
    #  ole_object.getBoundingClientRect.left.to_i
    #end

    #def left_edge_absolute
    #  left_edge + page_container.document.parentWindow.screenLeft.to_i
    #end

    #def bottom_edge
    #  assert_target self
    #  ole_object.getBoundingClientRect.bottom.to_i
    #end

    #def bottom_edge_absolute
    #  bottom_edge + page_container.document.parentWindow.screenTop.to_i
    #end

    def left_click
      x, y = source_x_y(0)
      WindowsInput.move_mouse(x+2,y+2)
      WindowsInput.left_click
    end
  end
end