import {
  trigger,
  transition,
  style,
  query,
  group,
  animateChild,
  animate,
  keyframes,
  state,
} from "@angular/animations";

// Basic
export const slideInOut = [
  trigger("slideInOut", [
    state(
      "in",
      style({
        "max-height": "500px",
        opacity: "1",
        visibility: "visible",
      })
    ),
    state(
      "out",
      style({
        "max-height": "0px",
        opacity: "0",
        visibility: "hidden",
      })
    ),
    transition("in => out", [
      group([
        animate(
          "400ms ease-in-out",
          style({
            opacity: "0",
          })
        ),
        animate(
          "600ms ease-in-out",
          style({
            "max-height": "0px",
          })
        ),
        animate(
          "700ms ease-in-out",
          style({
            visibility: "hidden",
          })
        ),
      ]),
    ]),
    transition("out => in", [
      group([
        animate(
          "1ms ease-in-out",
          style({
            visibility: "visible",
          })
        ),
        animate(
          "600ms ease-in-out",
          style({
            "max-height": "500px",
          })
        ),
        animate(
          "800ms ease-in-out",
          style({
            opacity: "1",
          })
        ),
      ]),
    ]),
  ]),
];

export const fader = trigger("routeAnimations", [
  transition("Home <=> *", [
    style({ opacity: 0 }),
    animate(400, style({ opacity: 1 })),
  ]),
]);

// Positioned

export const slider = trigger("routeAnimations", [
  transition("* => isLeft", slideTo("left")),
  transition("* => isRight", slideTo("right")),
  transition("isRight => *", slideTo("left")),
  transition("isLeft => *", slideTo("right")),
]);

export const transformer = trigger("routeAnimations", [
  transition("* => isLeft", translateTo({ x: -100, y: -100, rotate: -720 })),
  transition("* => isRight", translateTo({ x: 100, y: -100, rotate: 90 })),
  transition("isRight => *", translateTo({ x: -100, y: -100, rotate: 360 })),
  transition("isLeft => *", translateTo({ x: 100, y: -100, rotate: -360 })),
]);

function slideTo(direction) {
  const optional = { optional: true };
  return [
    query(
      ":enter, :leave",
      [
        style({
          position: "absolute",
          top: 0,
          [direction]: 0,
          width: "100%",
        }),
      ],
      optional
    ),
    query(":enter", [style({ [direction]: "-100%" })]),
    group([
      query(
        ":leave",
        [animate("600ms ease", style({ [direction]: "100%" }))],
        optional
      ),
      query(":enter", [animate("600ms ease", style({ [direction]: "0%" }))]),
    ]),
    // Normalize the page style... Might not be necessary

    // Required only if you have child animations on the page
    // query(':leave', animateChild()),
    // query(':enter', animateChild()),
  ];
}

function translateTo({ x = 100, y = 0, rotate = 0 }) {
  const optional = { optional: true };
  return [
    query(
      ":enter, :leave",
      [
        style({
          position: "absolute",
          top: 0,
          left: 0,
          width: "100%",
        }),
      ],
      optional
    ),
    query(":enter", [
      style({ transform: `translate(${x}%, ${y}%) rotate(${rotate}deg)` }),
    ]),
    group([
      query(
        ":leave",
        [
          animate(
            "600ms ease-out",
            style({ transform: `translate(${x}%, ${y}%) rotate(${rotate}deg)` })
          ),
        ],
        optional
      ),
      query(":enter", [
        animate(
          "600ms ease-out",
          style({ transform: `translate(0, 0) rotate(0)` })
        ),
      ]),
    ]),
  ];
}

// Keyframes

export const stepper = trigger("routeAnimations", [
  transition("* <=> *", [
    query(":enter, :leave", [
      style({
        position: "absolute",
        left: 0,
        width: "100%",
      }),
    ]),
    group([
      query(":enter", [
        animate(
          "2000ms ease",
          keyframes([
            style({ transform: "scale(0) translateX(100%)", offset: 0 }),
            style({ transform: "scale(0.5) translateX(25%)", offset: 0.3 }),
            style({ transform: "scale(1) translateX(0%)", offset: 1 }),
          ])
        ),
      ]),
      query(":leave", [
        animate(
          "2000ms ease",
          keyframes([
            style({ transform: "scale(1)", offset: 0 }),
            style({
              transform: "scale(0.5) translateX(-25%) rotate(0)",
              offset: 0.35,
            }),
            style({
              opacity: 0,
              transform: "translateX(-50%) rotate(-180deg) scale(6)",
              offset: 1,
            }),
          ])
        ),
      ]),
    ]),
  ]),
]);
