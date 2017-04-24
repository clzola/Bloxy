#include <cstdio>
#include <cstdlib>
#include <vector>
#include <cstring>
using namespace std;

struct block_t
{
    int x, y;
    short keyLen;
    char key[50];
    short nrectangles;
    vector<pair<int,int> > rectangles;
};

void writeBlock(block_t& b, FILE* f)
{
    fwrite(&(b.x), sizeof(int), 1, f);
    fwrite(&(b.y), sizeof(int), 1, f);
    fwrite(&(b.keyLen), sizeof(short), 1, f);
    fwrite(&(b.key), sizeof(char), b.keyLen, f);
    fwrite(&(b.nrectangles), sizeof(short), 1, f);

    for(vector<pair<int,int> >::iterator i = b.rectangles.begin(); i < b.rectangles.end(); i++) {
        fwrite(&(i->first), sizeof(int), 1, f);
        fwrite(&(i->second), sizeof(int), 1, f);
    }
}

int main()
{
    FILE* f = fopen("level1.bl", "wb");

    int Map[25][16] =
    {
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    };

    fwrite(Map, sizeof(int), 16*25, f);


    int nblocks = 3;
    fwrite(&nblocks, sizeof(int), 1, f);

    block_t b1;
    b1.x = 10 * 30;
    b1.y = 15 * 30;
    strcpy(b1.key, "blocks\\red");
    b1.keyLen = strlen(b1.key);
    b1.nrectangles = 4;
    b1.rectangles.push_back(make_pair(0,0));
    b1.rectangles.push_back(make_pair(0,1));
    b1.rectangles.push_back(make_pair(1,0));
    b1.rectangles.push_back(make_pair(1,1));
    writeBlock(b1,f);

    block_t b2;
    b2.x = 1 * 30;
    b2.y = 10 * 30;
    strcpy(b2.key, "blocks\\blue");
    b2.keyLen = strlen(b2.key);
    b2.nrectangles = 4;
    b2.rectangles.push_back(make_pair(0,0));
    b2.rectangles.push_back(make_pair(1,0));
    b2.rectangles.push_back(make_pair(2,0));
    b2.rectangles.push_back(make_pair(2,1));
    writeBlock(b2,f);

    block_t b3;
    b3.x = 5 * 30;
    b3.y = 20 * 30;
    strcpy(b3.key, "blocks\\green");
    b3.keyLen = strlen(b3.key);
    b3.nrectangles = 4;
    b3.rectangles.push_back(make_pair(1,0));
    b3.rectangles.push_back(make_pair(1,1));
    b3.rectangles.push_back(make_pair(1,2));
    b3.rectangles.push_back(make_pair(0,2));
    writeBlock(b3,f);

    fclose(f);
    return 0;
}
