#include <iostream>
#include "SDL.h"
#include <cmath>

using namespace std;

void bresenhamSimple(SDL_Renderer* renderer, int x1, int y1, int x2, int y2) {
	int dx = x2 - x1;
	int dy = y2 - y1;

	int error = (2 * dy) - dx;

	int y = y1;

	for (int x = x1; x < x2; x++) {
		SDL_RenderDrawPoint(renderer, x, y);
		if (error >= 0) {
			y = y + 1;
			error -= 2 * dx;
		}
		error += 2 * dy;
	}
}

void Bresenham_DrawLine(SDL_Renderer* renderer, int x1, int y1, int x2, int y2)
{
    int i, deltax, deltay, numpixels;
    int d, dinc1, dinc2;
    int x, xinc1, xinc2;
    int y, yinc1, yinc2;
    deltax = std::abs(x2 - x1);
    deltay = std::abs(y2 - y1);

    //Si es mas ancho que alto
    if (deltax >= deltay)
    {
        numpixels = deltax + 1;
        d = (2 * deltay) - deltax;
        dinc1 = deltay * 2;
        dinc2 = (deltay - deltax) * 2;
        xinc1 = 1;
        xinc2 = 1;
        yinc1 = 0;
        yinc2 = 1;
    }
    else
    {
        //Es mas alto que ancho
        numpixels = deltay + 1;
        d = (2 * deltax) - deltay;
        dinc1 = deltax * 2;
        dinc2 = (deltax - deltay) * 2;
        xinc1 = 0;
        xinc2 = 1;
        yinc1 = 1;
        yinc2 = 1;
    }
    //de izquierda a derecha
    if (x1 > x2)
    {
        xinc1 = -xinc1;
        xinc2 = -xinc2;
    }

    //de arriba a abajo
    if (y1 > y2)
    {
        yinc1 = -yinc1;
        yinc2 = -yinc2;
    }

    x = x1;
    y = y1;

    for (i = 0; i < numpixels; ++i)
    {
        SDL_RenderDrawPoint(renderer, x, y);
        if (d < 0)
        {
            d += dinc1;
            x += xinc1;
            y += yinc1;
        }
        else
        {
            d += dinc2;
            x += xinc2;
            y += yinc2;
        }
    }
}

void MakeRect(SDL_Renderer* renderer, int x, int y, int w, int h) {
	Bresenham_DrawLine(renderer, x, y, x + w, y);
	Bresenham_DrawLine(renderer, x + w, y, x + w, y + h);
	Bresenham_DrawLine(renderer, x, y, x, y + h);
	Bresenham_DrawLine(renderer, x, y + h, x + w, y + h);
}

void drawEllipse(SDL_Renderer* renderer, int x, int y, int a, int b) {
    long px = a * -1;
    long py = 0;
    long error2 = b;
    long dx = (1 + 2 * px) * error2 * error2;
    long dy = px * px;
    long error = dx + dy;

    SDL_RenderDrawPoint(renderer, x - px, y + py);
    SDL_RenderDrawPoint(renderer, x + px, y + py);
    SDL_RenderDrawPoint(renderer, x + px, y - py);
    SDL_RenderDrawPoint(renderer, x - px, y - py);

    while (px <= 0) {
        error2 = error * 2;
        if (error2 >= dx) {
            px++;
            error += dx;
            dx += 2 * (long)b * b;
        }
        if (error2 <= dy) {
            py++;
            error += dy;
            dy += 2 * (long)a * a;
        }

        SDL_RenderDrawPoint(renderer, x - px, y + py);
        SDL_RenderDrawPoint(renderer, x + px, y + py);
        SDL_RenderDrawPoint(renderer, x + px, y - py);
        SDL_RenderDrawPoint(renderer, x - px, y - py);

       

    }  
    while (py < b) {
       py++;
       SDL_RenderDrawPoint(renderer, x, y + py);
       SDL_RenderDrawPoint(renderer, x, y - py);
    }
}

void QuadraticBezierCurve(SDL_Renderer* renderer, double p0[], double p1[], double p2[], double t_lapse) {
    double pFinal[2] = { 0, 0 };
    double t = 0;
    while (t <= 1) {
        pFinal[0] = pow(1 - t, 2) * p0[0];
        pFinal[0] += (1 - t) * 2 * t * p1[0];
        pFinal[0] += t * t * p2[0];

        pFinal[1] = pow(1 - t, 2) * p0[1];
        pFinal[1] += (1 - t) * 2 * t * p1[1];
        pFinal[1] += t * t * p2[1];

        SDL_RenderDrawPoint(renderer, pFinal[0], pFinal[1]);

   

        t += t_lapse;
    }


}

void CubicBezierCurve(SDL_Renderer* renderer, double p0[], double p1[], double p2[], double p3[], double t_lapse) {
    double pFinal[2] = { 0, 0 };
    float t = 0;
    while (t <= 1) {
        pFinal[0] = pow(1 - t, 3) * p0[0];
        pFinal[0] += pow(1 - t, 2) * 3 * t * p1[0];
        pFinal[0] += (1 - t) * 3 * t * t * p2[0];
        pFinal[0] += t * t * t * p3[0];

        pFinal[1] = pow(1 - t, 3) * p0[1];
        pFinal[1] += pow(1 - t, 2) * 3 * t * p1[1];
        pFinal[1] += (1 - t) * 3 * t * t * p2[1];
        pFinal[1] += t * t * t * p3[1];

        SDL_RenderDrawPoint(renderer, pFinal[0], pFinal[1]);

      

        t += t_lapse;
    }


}

void DrawRegularPolygon(SDL_Renderer* renderer, int x, int y, int r, int sides) {
    drawEllipse(renderer, x, y, r, r);
    float angle = 2 * M_PI / sides;
    float angle_actual = 0;
    float x1, x2, y1, y2;

    for (int i = 0; i < sides; i++, angle_actual += angle) {

        x1 = x + (r * cos(angle_actual));
        y1 = y + (r * sin(angle_actual));
   
        x2 = x + (r * cos(angle_actual + angle));
        y2 = y + (r * sin(angle_actual + angle));

        SDL_RenderDrawLine(renderer, x1, y1, x2, y2);
    }
}



int main(int argc, char* argv[]) {
	SDL_Init(SDL_INIT_EVERYTHING);
	SDL_Window* window = SDL_CreateWindow("First Session", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 800, 600, SDL_WINDOW_SHOWN);
	SDL_Renderer* renderer = SDL_CreateRenderer(window, -1, 0);
    SDL_Renderer* renderer1 = SDL_CreateRenderer(window, -1, 0);

	SDL_SetRenderDrawColor(renderer, 233, 223, 223, 255);
	SDL_RenderClear(renderer);
	SDL_SetRenderDrawColor(renderer, 255, 0, 255, 255);

    SDL_Texture* texture = SDL_CreateTexture(renderer, SDL_PIXELFORMAT_RGBA8888, SDL_TEXTUREACCESS_TARGET, 800, 600);


	//bresenhamSimple(renderer, 0, 0, 800, 20);
	//MakeRect(renderer, 200, 100, 100, 100);
    //drawEllipse(renderer, 100, 200, 50, 20);
    
    double p0[2] = { 50 , 400 };
    double p1[2] = { 100, 100 };
    double p2[2] = { 300 , 300 };
    double p3[2] = { 500 , 500 };

    //QuadraticBezierCurve(renderer, p0, p1, p2, 0.001f);
    //CubicBezierCurve(renderer, p0, p1, p2, p3, 0.001f);

    //DrawRegularPolygon(renderer, 200, 200, 100, 5);


    bool drawing = false;
    int x1, x2;
    int y1, y2;
    SDL_Event event;
    enum Method {LINE, ELIPSES, POLYGON, IDLE};
    Method method = IDLE;

    cout << "Press Q to draw lines, W to draw ellipses, E to draw polygons!" << endl;

    while (true) {
        while (SDL_PollEvent(&event)) {

            if (event.type == SDLK_q) {
                cout << "You can now draw a line." << endl;
                method = LINE;
            }
            else if (event.type == SDLK_w) {
                cout << "You can now draw an ellipse." << endl;
                method = ELIPSES;
            }
            else if (event.type == SDLK_e) {
                cout << "You can now draw an ellipse." << endl;
                method = POLYGON;
            }
            
            switch (method) {
                case LINE:
                    if (drawing == false && event.type == SDL_MOUSEBUTTONDOWN) {
                        cout << "touch!" << endl;
                        SDL_GetMouseState(&x1, &y1);
                        SDL_RenderDrawPoint(renderer, x1, y1);
                        drawing = true;
                    }
                    else if (drawing == true) {
                        if (event.motion.y < 600 && event.motion.x < 800) {
                            x2 = event.motion.x;
                            y2 = event.motion.y;
                            Bresenham_DrawLine(renderer, x1, y1, x2, y2);
                            if (event.type == SDL_MOUSEBUTTONDOWN) {
                                SDL_RenderDrawPoint(renderer, x2, y2);
                                Bresenham_DrawLine(renderer, x1, y1, x2, y2);
                                drawing = false;
                            }
                        }
                        else drawing = false;
                    }
                break;
            }
            SDL_RenderPresent(renderer);
            SDL_SetRenderDrawColor(renderer, 233, 223, 223, 255);
            SDL_RenderClear(renderer);
            SDL_SetRenderDrawColor(renderer, 255, 0, 255, 255);
        }

    }
	SDL_Delay(20000);
	return 0;
}

