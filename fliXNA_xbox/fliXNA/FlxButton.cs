using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fliXNA_xbox
{
 
    enum ButtonStatus 
    { 
        NORMAL, HIGHLIGHT, PRESSED 
    }  

    public delegate void FlxButtonCallback();

    public class FlxButton : FlxSprite
    {
        protected Texture2D ImgDefaultButton = FlxG.content.Load<Texture2D>("button");
        //protected FlxSound SndBeep = FlxG.content.Load<FlxSound>("beep");

        public FlxText label;
        public FlxPoint labelOffset;

        private ButtonStatus status;

        protected FlxButtonCallback OnUp;
        protected FlxButtonCallback OnDown;
        protected FlxButtonCallback OnOver;
        protected FlxButtonCallback OnOut;

        public FlxSound soundOver;
        public FlxSound soundOut;
        public FlxSound soundDown;
        public FlxSound soundUp;

        public bool _onToggle;
        public bool _pressed;
        public bool _initialized;


        /**
        * Creates a new <code>FlxButton</code> object with a gray background
        * and a callback function on the UI thread.
        *
        * @param X The X position of the button.
        * @param Y The Y position of the button.
        * @param Label The text that you want to appear on the button.
        * @param OnClick The function to call whenever the button is clicked.
        */
        public FlxButton(int X, int Y, string Label = null, FlxButtonCallback OnClick = null)
            : base()
        {
            x = X;
            y = Y;
            if (Label != null)
            {
                label = new FlxText(0, 0, 80, Label);
                label.setFormat(new Color(31, 31, 31), 8, null);
                labelOffset = new FlxPoint(-1, 3);
            }
            loadGraphic(ImgDefaultButton, true, false, 80, 20);

            OnUp = OnClick;
            OnDown = null;
            OnOut = null;
            OnOver = null;

            soundOver = null;
            soundOut = null;
            soundDown = null;
            soundUp = null;

            status = ButtonStatus.NORMAL;
            _onToggle = false;
            _pressed = false;
            _initialized = false;
        }

        /**
        * Called by the game state when state is changed (if this object belongs to the state)
        */
        public override void destroy()
        {
            /*
            if(FlxG.stage != null)
            FlxG.stage.removeEventListener(MouseEvent.MOUSE_UP, onMouseUp);
             */
            if(label != null)
            {
            label.destroy();
            label = null;
            }
            OnUp = null;
            OnDown = null;
            OnOut = null;
            OnOver = null;
            if(soundOver != null)
            soundOver.destroy();
            if(soundOut != null)
            soundOut.destroy();
            if(soundDown != null)
            soundDown.destroy();
            if(soundUp != null)
            soundUp.destroy();
            base.destroy();
        }

        public override void preUpdate()
        {
            base.preUpdate();

            if (!_initialized)
            {
                if (FlxG.mouse != null)
                {
                    
                    _initialized = true;
                }

            }
        }

        public override void update()
        {
            updateButton();

            if (label == null)
                return;
            switch ((ButtonStatus)frame)
            { 
                case ButtonStatus.HIGHLIGHT:
                    label.alpha = 1.0f;
                    break;
                case ButtonStatus.PRESSED:
                    label.alpha = 0.5f;
                    label.y++;
                    break;
                case ButtonStatus.NORMAL:
                default:
                    label.alpha = 0.8f;
                    break;
            }

        }

        protected void updateButton()
        {
            //if (cameras == null)
            //    cameras = FlxG.cameras;
            //FlxCamera camera;
            //int i = 0;
            //int l = cameras.Count;
            //bool offAll = true;
            //while ( i < l )
            //{
            //    camera = cameras[i++] as FlxCamera;
            //    FlxG.mouse.screenX
            //}


            //Then if the label and/or the label offset exist,
            // position them to match the button.
            if (label != null)
            {
                label.x = x;
                label.y = y;
            }
            if (labelOffset != null)
            {
                label.x += labelOffset.x;
                label.y += labelOffset.y;
            }

            //Then pick the appropriate frame of animation
            if ((status == ButtonStatus.HIGHLIGHT) && _onToggle)
                frame = (int)ButtonStatus.NORMAL;
            else
                frame = (int)status;
        }

        public override void draw()
        {
            base.draw();
            if (label != null)
            {
                label.scrollFactor = scrollFactor;
                label.cameras = cameras;
                label.draw();
            }

        }

    }
}
