#!/bin/bash
#!/bin/bash
# declare -a arr=("size_5" "size_10" "size_20" "size_50" "size_100")
declare -a arr=("size_5")

for i in "${arr[@]}"
do 
    rm datasets/seadronesee/labels/*.cache 2> /dev/null
    mv datasets/seadronesee/labels/val_coco datasets/seadronesee/labels/val

    echo "VALIDATING :::: $i"
    CROSS=$i
    LIBPATH=lib/yolov5-master
    NAME=coco_pretrained_sds_${CROSS}
    MODELPATH=$LIBPATH/runs/train/$NAME/weights/best.pt # <--- MODEL
    # MODELPATH=$LIBPATH/yolov5s.pt # <--- MODEL

    python $LIBPATH/val.py \
        --data datasets/seadronesee/data_coco.yaml \
        --name ${NAME}_sds \
        --weights $MODELPATH \
        --save-txt \
        --save-json \
        --save-conf

    echo ".... renaming....."
    mv datasets/seadronesee/labels/val datasets/seadronesee/labels/val_coco
    echo "ENDED WITH SUCCESS!"
done