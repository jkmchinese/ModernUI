using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using ModernUI.ExtendedToolkit.Internal;

namespace ModernUI.ExtendedToolkit
{
	/// <summary>
	/// Positions child elements in sequential position from left to right, breaking content 
	/// to the next line at the edge of the containing box. Subsequent ordering happens 
	/// sequentially from top to bottom or from right to left, depending on the value of 
	/// the Orientation property.
	/// </summary>
	[System.ComponentModel.DefaultProperty("Orientation")]
	public class VirtualizingWrapPanel : VirtualizingPanel, IScrollInfo, IPanelKeyboardHelper
	{
		/// <summary>
		/// Identifies the ItemHeight dependency property.
		/// </summary>
		public static readonly DependencyProperty ItemHeightProperty =
			DependencyProperty.Register("ItemHeight",
				typeof(double),
				typeof(VirtualizingWrapPanel),
				new PropertyMetadata(100d, OnAppearancePropertyChanged));

		/// <summary>
		/// Identifies the Orientation dependency property.
		/// </summary>
		public static readonly DependencyProperty OrientationProperty =
			DependencyProperty.Register("Orientation",
				typeof(Orientation),
				typeof(VirtualizingWrapPanel),
				new PropertyMetadata(Orientation.Horizontal, OnAppearancePropertyChanged));

		/// <summary>
		/// Identifies the ItemWidth dependency property.
		/// </summary>
		public static readonly DependencyProperty ItemWidthProperty =
			DependencyProperty.Register("ItemWidth",
				typeof(double),
				typeof(VirtualizingWrapPanel),
				new PropertyMetadata(100d, OnAppearancePropertyChanged));

		/// <summary>
		/// Identifies the ScrollStep dependency property.
		/// </summary>
		public static readonly DependencyProperty ScrollStepProperty =
			DependencyProperty.Register("ScrollStep",
				typeof(double),
				typeof(VirtualizingWrapPanel),
				new PropertyMetadata(10d, OnAppearancePropertyChanged));

		private int itemsCount;
		private bool canHorizontallyScroll = false;
		private bool canVerticallyScroll = false;
		private Size contentExtent = new Size(0, 0);
		private Point contentOffset = new Point();
		private ScrollViewer scrollOwner;
		private Size viewport = new Size(0, 0);
		private int previousItemCount;

		/// <summary>
		/// Gets or sets a value that specifies the height of all items that are 
		/// contained within a VirtualizingWrapPanel. This is a dependency property.
		/// </summary>
		public double ItemHeight
		{
			get
			{
				return (double)GetValue(ItemHeightProperty);
			}
			set
			{
				SetValue(ItemHeightProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets a value that specifies the width of all items that are 
		/// contained within a VirtualizingWrapPanel. This is a dependency property.
		/// </summary>
		public double ItemWidth
		{
			get
			{
				return (double)GetValue(ItemWidthProperty);
			}
			set
			{
				SetValue(ItemWidthProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets a value that specifies the dimension in which child 
		/// content is arranged. This is a dependency property.
		/// </summary>
		public Orientation Orientation
		{
			get
			{
				return (Orientation)this.GetValue(OrientationProperty);
			}
			set
			{
				this.SetValue(OrientationProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets a value that indicates whether scrolling on the horizontal axis is possible.
		/// </summary>
		public bool CanHorizontallyScroll
		{
			get
			{
				return this.canHorizontallyScroll;
			}
			set
			{
				if (this.canHorizontallyScroll != value)
				{
					this.canHorizontallyScroll = value;

					this.InvalidateMeasure();
				}
			}
		}

		/// <summary>
		/// Gets or sets a value that indicates whether scrolling on the vertical axis is possible.
		/// </summary>
		public bool CanVerticallyScroll
		{
			get
			{
				return canVerticallyScroll;
			}
			set
			{
				if (this.canVerticallyScroll != value)
				{
					this.canVerticallyScroll = value;

					this.InvalidateMeasure();
				}
			}
		}

		/// <summary>
		/// Gets or sets a ScrollViewer element that controls scrolling behavior.
		/// </summary>
		public ScrollViewer ScrollOwner
		{
			get
			{
				return this.scrollOwner;
			}
			set
			{
				this.scrollOwner = value;
			}
		}

		/// <summary>
		/// Gets the vertical offset of the scrolled content.
		/// </summary>
		public double VerticalOffset
		{
			get
			{
				return this.contentOffset.Y;
			}
		}

		/// <summary>
		/// Gets the vertical size of the viewport for this content.
		/// </summary>
		public double ViewportHeight
		{
			get
			{
				return this.viewport.Height;
			}
		}

		/// <summary>
		/// Gets the horizontal size of the viewport for this content.
		/// </summary>
		public double ViewportWidth
		{
			get
			{
				return this.viewport.Width;
			}
		}

		/// <summary>
		/// Gets or sets a value for mouse wheel scroll step.
		/// </summary>
		public double ScrollStep
		{
			get
			{
				return (double)this.GetValue(ScrollStepProperty);
			}
			set
			{
				SetValue(ScrollStepProperty, value);
			}
		}

		/// <summary>
		/// Gets the vertical size of the extent.
		/// </summary>
		public double ExtentHeight
		{
			get
			{
				return this.contentExtent.Height;
			}
		}

		/// <summary>
		/// Gets the horizontal size of the extent.
		/// </summary>
		public double ExtentWidth
		{
			get
			{
				return this.contentExtent.Width;
			}
		}

		/// <summary>
		/// Gets the horizontal offset of the scrolled content.
		/// </summary>
		public double HorizontalOffset
		{
			get
			{
				return this.contentOffset.X;
			}
		}

		IPanelHelper IPanelKeyboardHelper.PanelHelper
		{
			get;
			set;
		}

		/// <summary>
		/// Scrolls down within content by one logical unit.
		/// </summary>
		public void LineDown()
		{
			this.SetVerticalOffset(this.VerticalOffset + this.ScrollStep);
		}

		/// <summary>
		/// Scrolls left within content by one logical unit.
		/// </summary>
		public void LineLeft()
		{
			this.SetHorizontalOffset(this.HorizontalOffset - this.ScrollStep);
		}

		/// <summary>
		/// Scrolls right within content by one logical unit.
		/// </summary>
		public void LineRight()
		{
			this.SetHorizontalOffset(this.HorizontalOffset + this.ScrollStep);
		}

		/// <summary>
		/// Scrolls up within content by one logical unit.
		/// </summary>
		public void LineUp()
		{
			this.SetVerticalOffset(this.VerticalOffset - this.ScrollStep);
		}

		/// <summary>
		/// Forces content to scroll until the coordinate space of a Visual object is visible.
		/// </summary>
		public Rect MakeVisible(Visual visual, Rect rectangle)
		{
			this.MakeVisible(visual as UIElement);

			return rectangle;
		}

        /// <summary>
        /// Forces content to scroll until the coordinate space of a UIElement object is visible.
        /// </summary>
        public Rect MakeVisible(UIElement visual, Rect rectangle)
        {
            this.MakeVisible(visual);

            return rectangle;
        }

		/// <summary>
		/// Scrolls down within content after a user clicks the wheel button on a mouse.
		/// </summary>
		public void MouseWheelDown()
		{
			this.SetVerticalOffset(this.VerticalOffset + this.ScrollStep);
		}

		/// <summary>
		/// Scrolls left within content after a user clicks the wheel button on a mouse.
		/// </summary>
		public void MouseWheelLeft()
		{
			this.SetHorizontalOffset(this.HorizontalOffset - this.ScrollStep);
		}

		/// <summary>
		/// Scrolls right within content after a user clicks the wheel button on a mouse.
		/// </summary>
		public void MouseWheelRight()
		{
			this.SetHorizontalOffset(this.HorizontalOffset + this.ScrollStep);
		}

		/// <summary>
		/// Scrolls up within content after a user clicks the wheel button on a mouse.
		/// </summary>
		public void MouseWheelUp()
		{
			this.SetVerticalOffset(this.VerticalOffset - this.ScrollStep);
		}

		/// <summary>
		/// Scrolls down within content by one page.
		/// </summary>
		public void PageDown()
		{
			this.SetVerticalOffset(this.VerticalOffset + this.ViewportHeight);
		}

		/// <summary>
		/// Scrolls left within content by one page.
		/// </summary>
		public void PageLeft()
		{
			this.SetHorizontalOffset(this.HorizontalOffset - this.ViewportHeight);
		}

		/// <summary>
		/// Scrolls right within content by one page.
		/// </summary>
		public void PageRight()
		{
			this.SetHorizontalOffset(this.HorizontalOffset + this.ViewportHeight);
		}

		/// <summary>
		/// Scrolls up within content by one page.
		/// </summary>
		public void PageUp()
		{
			this.SetVerticalOffset(this.VerticalOffset - this.viewport.Height);
		}

		/// <summary>
		/// Sets the amount of vertical offset.
		/// </summary>
		public void SetVerticalOffset(double offset)
		{
			if (offset < 0 || this.ViewportHeight >= this.ExtentHeight)
			{
				offset = 0;
			}
			else
			{
				if (offset + this.ViewportHeight >= this.ExtentHeight)
				{
					offset = this.ExtentHeight - this.ViewportHeight;
				}
			}

			this.contentOffset.Y = offset;

			if (this.ScrollOwner != null)
			{
				this.ScrollOwner.InvalidateScrollInfo();
			}

			this.InvalidateMeasure();
		}

		/// <summary>
		/// Sets the amount of horizontal offset.
		/// </summary>
		public void SetHorizontalOffset(double offset)
		{
			if (offset < 0 || this.ViewportWidth >= this.ExtentWidth)
			{
				offset = 0;
			}
			else
			{
				if (offset + this.ViewportWidth >= this.ExtentWidth)
				{
					offset = this.ExtentWidth - this.ViewportWidth;
				}
			}

			this.contentOffset.X = offset;

			if (this.ScrollOwner != null)
			{
				this.ScrollOwner.InvalidateScrollInfo();
			}

			this.InvalidateMeasure();
		}

		/// <summary>
		/// Note: Works only for vertical.
		/// </summary>
		internal void PageLast()
		{
			this.contentOffset.Y = this.ExtentHeight;

			if (this.ScrollOwner != null)
			{
				this.ScrollOwner.InvalidateScrollInfo();
			}

			this.InvalidateMeasure();
		}

		/// <summary>
		/// Note: Works only for vertical.
		/// </summary>
		internal void PageFirst()
		{
			this.contentOffset.Y = 0.0d;

			if (this.ScrollOwner != null)
			{
				this.ScrollOwner.InvalidateScrollInfo();
			}

			this.InvalidateMeasure();
		}

		/// <summary>
		/// When items are removed, remove the corresponding UI if necessary.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		protected override void OnItemsChanged(object sender, ItemsChangedEventArgs args)
		{
			switch (args.Action)
			{
				case NotifyCollectionChangedAction.Remove:
				case NotifyCollectionChangedAction.Replace:
				case NotifyCollectionChangedAction.Move:
					RemoveInternalChildRange(args.Position.Index, args.ItemUICount);
					break;
				case NotifyCollectionChangedAction.Reset:

					var itemsControl = System.Windows.Controls.ItemsControl.GetItemsOwner(this);
					if (itemsControl != null)
					{
						if (previousItemCount != itemsControl.Items.Count)
						{
							if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
							{
								this.SetVerticalOffset(0);
							}
							else
							{
								this.SetHorizontalOffset(0);
							}
						}

						previousItemCount = itemsControl.Items.Count;
					}

					break;
			}
		}

		/// <summary>
		/// Measure the children.
		/// </summary>
		/// <param name="availableSize">The available size.</param>
		/// <returns>The desired size.</returns>
		protected override Size MeasureOverride(Size availableSize)
		{
			this.InvalidateScrollInfo(availableSize);

			int firstVisibleIndex, lastVisibleIndex;

			if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
			{
				this.GetVerticalVisibleRange(out firstVisibleIndex, out lastVisibleIndex);
			}
			else
			{
				this.GetHorizontalVisibleRange(out firstVisibleIndex, out lastVisibleIndex);
			}

			var children = this.Children;
			var generator = this.ItemContainerGenerator;

			if (generator != null)
			{
				var startPos = generator.GeneratorPositionFromIndex(firstVisibleIndex);

				var childIndex = (startPos.Offset == 0) ? startPos.Index : startPos.Index + 1;

				if (childIndex == -1)
				{
					this.RefreshOffset();
				}

				using (generator.StartAt(startPos, GeneratorDirection.Forward, true))
				{
					for (var itemIndex = firstVisibleIndex; itemIndex <= lastVisibleIndex; itemIndex++, childIndex++)
					{
						bool newlyRealized;

						var child = generator.GenerateNext(out newlyRealized) as UIElement;
						if (newlyRealized)
						{
							if (childIndex >= children.Count)
							{
								this.AddInternalChild(child);
							}
							else
							{
								this.InsertInternalChild(childIndex, child);
							}

							generator.PrepareItemContainer(child);
						}

						if (child != null)
						{
							child.Measure(new Size(this.ItemWidth, this.ItemHeight));
						}
					}
				}

				this.CleanUpChildren(firstVisibleIndex, lastVisibleIndex);
			}

			Size adjustedSize = availableSize;

			if (double.IsPositiveInfinity(availableSize.Width))
			{
				adjustedSize = new Size(this.GetExtent(adjustedSize, this.itemsCount).Width, adjustedSize.Height);
			}
			if (double.IsPositiveInfinity(availableSize.Height))
			{
				adjustedSize = new Size(adjustedSize.Width, this.GetExtent(adjustedSize, itemsCount).Height);
			}

			return adjustedSize;
		}

		/// <summary>
		/// Arranges the children.
		/// </summary>
		/// <param name="finalSize">The available size.</param>
		/// <returns>The used size.</returns>
		protected override Size ArrangeOverride(Size finalSize)
		{
			var isHorizontal = this.Orientation == System.Windows.Controls.Orientation.Horizontal;

			this.InvalidateScrollInfo(finalSize);
			int i = 0;

			foreach (var item in this.Children)
			{
				this.ArrangeChild(isHorizontal, finalSize, i++, item as UIElement);
			}

			return finalSize;
		}

		private static void OnAppearancePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var panel = d as UIElement;
			if (panel != null)
			{
				panel.InvalidateMeasure();
			}
		}

		private void MakeVisible(UIElement element)
		{
			var generator = this.ItemContainerGenerator.GetItemContainerGeneratorForPanel(this);

			if (element != null && generator != null)
			{
				int itemIndex = generator.IndexFromContainer(element);

				// Try to get the real item if the current is some child of the real item
				while (itemIndex == -1)
				{
					element = element.ParentOfType<UIElement>();
					itemIndex = generator.IndexFromContainer(element);
				}

				var scrollViewer = element.ParentOfType<ScrollViewer>();
				if (scrollViewer != null)
				{
					var elementTransform = element.TransformToVisual(scrollViewer);
					var elementRectangle = elementTransform.TransformBounds(new Rect(new Point(0, 0), element.RenderSize));

					if (Orientation == Orientation.Horizontal)
					{
						if (elementRectangle.Bottom > this.ViewportHeight)
						{
							this.SetVerticalOffset(contentOffset.Y + elementRectangle.Bottom - this.ViewportHeight);
						}
						else if (elementRectangle.Top < 0)
						{
							this.SetVerticalOffset(contentOffset.Y + elementRectangle.Top);
						}
					}
					else
					{
						if (elementRectangle.Right > this.ViewportWidth)
						{
							this.SetHorizontalOffset(contentOffset.X + elementRectangle.Right - this.ViewportWidth);
						}
						else if (elementRectangle.Left < 0)
						{
							this.SetHorizontalOffset(contentOffset.X + elementRectangle.Left);
						}
					}
				}
			}
		}

		private void GetVerticalVisibleRange(out int firstVisibleItemIndex, out int lastVisibleItemIndex)
		{
			var childrenPerRow = this.GetVerticalChildrenCountPerRow(contentExtent);

			firstVisibleItemIndex = (int)Math.Floor(this.VerticalOffset / this.ItemHeight) * childrenPerRow;

			if (double.IsInfinity(this.ViewportHeight))
			{
				lastVisibleItemIndex = this.itemsCount - 1;
			}
			else
			{
				lastVisibleItemIndex = ((int)Math.Ceiling((this.VerticalOffset + this.ViewportHeight) / this.ItemHeight) * childrenPerRow) - 1;
			}

			AdjustVisibleRange(ref firstVisibleItemIndex, ref lastVisibleItemIndex);
		}

		private void GetHorizontalVisibleRange(out int firstVisibleItemIndex, out int lastVisibleItemIndex)
		{
			var childrenPerRow = this.GetHorizontalChildrenCountPerRow(contentExtent);

			firstVisibleItemIndex = (int)Math.Floor(this.HorizontalOffset / this.ItemWidth) * childrenPerRow;
			if (double.IsInfinity(this.ViewportWidth))
			{
				lastVisibleItemIndex = this.itemsCount;
			}
			else
			{
				lastVisibleItemIndex = ((int)Math.Ceiling((this.HorizontalOffset + this.ViewportWidth) / this.ItemWidth) * childrenPerRow) - 1;
			}

			AdjustVisibleRange(ref firstVisibleItemIndex, ref lastVisibleItemIndex);
		}

		private void AdjustVisibleRange(ref int firstVisibleItemIndex, ref int lastVisibleItemIndex)
		{
			firstVisibleItemIndex--;
			lastVisibleItemIndex++;

			var itemsControl = System.Windows.Controls.ItemsControl.GetItemsOwner(this);

			if (itemsControl != null)
			{
				if (firstVisibleItemIndex < 0)
				{
					firstVisibleItemIndex = 0;
				}

				if (lastVisibleItemIndex >= itemsControl.Items.Count)
				{
					lastVisibleItemIndex = itemsControl.Items.Count - 1;
				}
			}
		}

		private void CleanUpChildren(int minIndex, int maxIndex)
		{
			var children = this.Children;
			var generator = this.ItemContainerGenerator;

			for (var i = children.Count - 1; i >= 0; i--)
			{
				var pos = new GeneratorPosition(i, 0);
				var itemIndex = generator.IndexFromGeneratorPosition(pos);
				if (itemIndex < minIndex || itemIndex > maxIndex)
				{
					generator.Remove(pos, 1);
					RemoveInternalChildRange(i, 1);
				}
			}
		}

		private void ArrangeChild(bool isHorizontal, Size finalSize, int index, UIElement child)
		{
			if (child == null)
				return;

			var count = isHorizontal ? this.GetVerticalChildrenCountPerRow(finalSize) : this.GetHorizontalChildrenCountPerRow(finalSize);
			var itemIndex = this.ItemContainerGenerator.IndexFromGeneratorPosition(new GeneratorPosition(index, 0));

			var row = isHorizontal ? itemIndex / count : itemIndex % count;
			var column = isHorizontal ? itemIndex % count : itemIndex / count;

			var rect = new Rect(column * this.ItemWidth, row * this.ItemHeight, this.ItemWidth, this.ItemHeight);

			if (isHorizontal)
			{
				rect.Y -= this.VerticalOffset;
			}
			else
			{
				rect.X -= this.HorizontalOffset;
			}

			child.Arrange(rect);
		}

		private void InvalidateScrollInfo(Size availableSize)
		{
			var ownerItemsControl = System.Windows.Controls.ItemsControl.GetItemsOwner(this);

			if (ownerItemsControl != null)
			{
				itemsCount = ownerItemsControl.Items.Count;
				var extent = this.GetExtent(availableSize, itemsCount);

				if (extent != this.contentExtent)
				{
					this.contentExtent = extent;
					this.RefreshOffset();
				}

				if (double.IsPositiveInfinity(availableSize.Width) || double.IsPositiveInfinity(availableSize.Height))
				{
					return;
				}				

				if (availableSize != viewport)
				{
					this.viewport = availableSize;

					this.InvalidateScrollOwner();
					this.RefreshOffset();
				}
			}
		}

		private void RefreshOffset()
		{
			if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
			{
				this.SetVerticalOffset(this.VerticalOffset);
			}
			else
			{
				this.SetHorizontalOffset(this.HorizontalOffset);
			}
		}

		private void InvalidateScrollOwner()
		{
			if (this.ScrollOwner != null)
			{
				this.ScrollOwner.InvalidateScrollInfo();
			}
		}

		private Size GetExtent(Size availableSize, int itemCount)
		{
			if (this.Orientation == System.Windows.Controls.Orientation.Horizontal)
			{
				var childrenPerRow = this.GetVerticalChildrenCountPerRow(availableSize);

				return new Size(childrenPerRow * this.ItemWidth,
					this.ItemHeight * Math.Ceiling((double)itemCount / childrenPerRow));
			}
			else
			{
				var childrenPerRow = this.GetHorizontalChildrenCountPerRow(availableSize);

				return new Size(this.ItemWidth * Math.Ceiling((double)itemCount / childrenPerRow),
					childrenPerRow * this.ItemHeight);
			}
		}

		private int GetVerticalChildrenCountPerRow(Size availableSize)
		{
			var childrenCountPerRow = 0;

			if (availableSize.Width == double.PositiveInfinity)
			{
				childrenCountPerRow = this.Children.Count;
			}
			else
			{
				childrenCountPerRow = Math.Max(1, (int)Math.Floor(availableSize.Width / this.ItemWidth));
			}

			return childrenCountPerRow;
		}

		private int GetHorizontalChildrenCountPerRow(Size availableSize)
		{
			var childrenCountPerRow = 0;

			if (availableSize.Height == double.PositiveInfinity)
			{
				childrenCountPerRow = this.Children.Count;
			}
			else
			{
				childrenCountPerRow = Math.Max(1, (int)Math.Floor(availableSize.Height / this.ItemHeight));
			}

			return childrenCountPerRow;
		}

		Point IPanelKeyboardHelper.GetOffsets(int index)
		{
			var firstVisibleContainer = this.GetFirstContainerInViewport();
			var lastVisibleContainer = this.GetLastContainerInViewport();
			if (firstVisibleContainer != null && lastVisibleContainer != null)
			{
				var startIndex = ((System.Windows.Controls.ItemContainerGenerator)this.ItemContainerGenerator).IndexFromContainer(firstVisibleContainer);
				var lastIndex = ((System.Windows.Controls.ItemContainerGenerator)this.ItemContainerGenerator).IndexFromContainer(lastVisibleContainer);
				if (index >= startIndex && index <= lastIndex)
				{
					return new Point(this.HorizontalOffset, this.VerticalOffset);
				}
			}

			var rowVertIndex = (int)(index / this.GetVerticalChildrenCountPerRow(this.viewport));
			var verticalOffset = rowVertIndex * this.ItemHeight;
			var horizontalOffset = rowVertIndex * this.ItemWidth;

			var point = new Point(horizontalOffset, verticalOffset);
			if (verticalOffset + this.ItemHeight > this.VerticalOffset + this.ViewportHeight)
			{
				point.Y = verticalOffset - this.ViewportHeight + this.ItemHeight;
			}

			if (verticalOffset + this.ItemWidth > this.HorizontalOffset + this.ViewportWidth)
			{
				point.X = horizontalOffset - this.ViewportWidth + this.ItemWidth;
			}

			return point;
		}

		int IPanelKeyboardHelper.GetPageUpIndex(int fromIndex)
		{
			var firstVisibleContainer = this.GetFirstContainerInViewport();
			var lastVisibleContainer = this.GetLastContainerInViewport();
			if (firstVisibleContainer != null && lastVisibleContainer != null)
			{
				var startIndex = ((System.Windows.Controls.ItemContainerGenerator)this.ItemContainerGenerator).IndexFromContainer(firstVisibleContainer);
				var lastIndex = ((System.Windows.Controls.ItemContainerGenerator)this.ItemContainerGenerator).IndexFromContainer(lastVisibleContainer);
				if (startIndex != fromIndex)
				{
					return startIndex;
				}
			}
			var rowCount = this.GetHorizontalChildrenCountPerRow(this.viewport);
			var columnCount = this.GetVerticalChildrenCountPerRow(this.viewport);
			var index = fromIndex - (rowCount * columnCount);
			return index < 0 ? 0 : index;
		}

		int IPanelKeyboardHelper.GetPageDownIndex(int fromIndex)
		{
			var firstVisibleContainer = this.GetFirstContainerInViewport();
			var lastVisibleContainer = this.GetLastContainerInViewport();
			if (firstVisibleContainer != null && lastVisibleContainer != null)
			{
				var startIndex = ((System.Windows.Controls.ItemContainerGenerator)this.ItemContainerGenerator).IndexFromContainer(firstVisibleContainer);
				var lastIndex = ((System.Windows.Controls.ItemContainerGenerator)this.ItemContainerGenerator).IndexFromContainer(lastVisibleContainer);

				if (lastIndex != fromIndex)
				{
					return lastIndex;
				}
			}
			var rowCount = this.GetHorizontalChildrenCountPerRow(this.viewport);
			var columnCount = this.GetVerticalChildrenCountPerRow(this.viewport);
			var index = fromIndex + (rowCount * columnCount);
			return index > this.itemsCount - 1 ? this.itemsCount - 1 : index;
		}

		double IPanelKeyboardHelper.GetVerticalOffsetForTouch()
		{
			return this.VerticalOffset;
		}

		double IPanelKeyboardHelper.GetHorizontalOffsetForTouch()
		{
			return this.HorizontalOffset;
		}

		private bool IsInTheViewport(FrameworkElement item)
		{
			if (item == null)
			{
				return false;
			}

			var slot = (this as IPanelKeyboardHelper).PanelHelper.GetLayoutSlot(item);
			return slot.Y >= 0 &&
				slot.Height + slot.Y <= this.ViewportHeight &&
				slot.X >= 0 &&
				slot.Width + slot.X <= this.ViewportWidth;
		}

		private FrameworkElement GetFirstContainerInViewport()
		{
			return this.Children.Cast<FrameworkElement>().FirstOrDefault(item => this.IsInTheViewport(item));
		}

		private FrameworkElement GetLastContainerInViewport()
		{
			return this.Children.Cast<FrameworkElement>().LastOrDefault(item => this.IsInTheViewport(item));
		}
	}
}